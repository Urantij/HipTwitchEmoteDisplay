using System.Text.RegularExpressions;
using HipTwitchEmoteDisplay.Emotes;
using Microsoft.Extensions.Options;
using TwitchSimpleLib.Chat;
using TwitchSimpleLib.Chat.Messages;

namespace HipTwitchEmoteDisplay.Twitch;

/// <summary>
/// Сервис сидит в чате твича и выуживает эмоуты из сообщений.
/// </summary>
public partial class TwitchChatter : IHostedService
{
    private readonly EmoteVault _emoteVault;

    private readonly TwitchChatClient _client;
    private readonly ChatAutoChannel _channel;
    private readonly ILogger<TwitchChatter> _logger;

    private readonly Regex _emoteRegex = MyEmoteRegex();

    private readonly Regex _wordRegex = MyWordRegex();

    public event Action<ChatYes>? Yes;

    public TwitchChatter(EmoteVault emoteVault, IOptions<AppConfig> options, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TwitchChatter>();
        _emoteVault = emoteVault;
        _client = new TwitchChatClient(true, new TwitchChatClientOpts(), loggerFactory);
        _client.Connected += ClientOnConnected;

        _channel = _client.AddAutoJoinChannel(options.Value.TwitchUsername);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _channel.PrivateMessageReceived += ChannelOnPrivateMessageReceived;

        return _client.ConnectAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _client.Close();

        return Task.CompletedTask;
    }

    private void ClientOnConnected()
    {
        _logger.LogDebug("Присоединились.");
    }

    private void ChannelOnPrivateMessageReceived(object? sender, TwitchPrivateMessage e)
    {
        InTextEmote[] officialEmotes = GetEmotesFromMessage(e).ToArray();

        // Это крайне неэффективно, но я не в настроении делать идущий по строке цикл. TODO
        MatchCollection matches = _wordRegex.Matches(e.text);

        // xdd

        int emoteNum = 0;
        EmoteInstance[] instances = matches.Select(match =>
            {
                InTextEmote? ofEmoteInstance = officialEmotes.FirstOrDefault(ofE => ofE.Start == match.Index);

                if (ofEmoteInstance != null)
                    return ofEmoteInstance;

                Emote? emote = _emoteVault.TryGetEmote(match.Value);

                return emote == null ? null : new InTextEmote(emote, match.Index);
            })
            .GroupBy(inTextEmote =>
            {
                if (inTextEmote == null)
                {
                    emoteNum++;
                    return -1;
                }

                if (!inTextEmote.Emote.ZeroWidth)
                    emoteNum++;

                return emoteNum;
            })
            .Where(g => g.Key != -1)
            .Select(g => new EmoteInstance(g.Select(ite => ite.Emote).ToArray()))
            .ToArray();

        // InTextEmote[] instances = matches.Select(match =>
        //     {
        //         Emote? emote = _emoteVault.TryGetEmote(match.Value);
        //         return emote == null ? null : new InTextEmote(emote, match.Index);
        //     })
        //     .Where(instance =>
        //         instance != null &&
        //         officialEmotes.All(officialInstance => officialInstance.Start != instance.Start))
        //     .Select(instance => instance!)
        //     .Concat(officialEmotes)
        //     .ToArray();

        if (instances.Length == 0)
            return;

        Yes?.Invoke(new ChatYes(e.username, instances));
    }

    private IEnumerable<InTextEmote> GetEmotesFromMessage(TwitchPrivateMessage e)
    {
        if (e.rawIrcMessage.tags?.TryGetValue("emotes", out string? emoteString) != true || emoteString == null)
            yield break;

        MatchCollection matches = _emoteRegex.Matches(emoteString);

        foreach (Match match in matches)
        {
            string emoteId = match.Groups["id"].Value;
            int start = int.Parse(match.Groups["start"].Value);
            int end = int.Parse(match.Groups["end"].Value);

            string emoteKey = e.text.Substring(start, end - start + 1);

            Emote emote = _emoteVault.MakeEmoteFromId(emoteKey, emoteId);

            yield return new InTextEmote(emote, start);
        }
    }

    [GeneratedRegex(@"(?<id>.+?):(?<start>\d+)-(?<end>\d+)", RegexOptions.Compiled)]
    private static partial Regex MyEmoteRegex();

    [GeneratedRegex(@"[^\s]+?(?=\s|$)")]
    private static partial Regex MyWordRegex();
}