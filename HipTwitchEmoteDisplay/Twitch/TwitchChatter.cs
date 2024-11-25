using HipTwitchEmoteDisplay.Emotes;
using Microsoft.Extensions.Options;
using TwitchSimpleLib.Chat;
using TwitchSimpleLib.Chat.Messages;

namespace HipTwitchEmoteDisplay.Twitch;

public class TwitchChatter : IHostedService
{
    private readonly EmoteVault _emoteVault;

    private readonly TwitchChatClient _client;
    private readonly ChatAutoChannel _channel;
    private readonly ILogger<TwitchChatter> _logger;

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
        Emote[] emotes = e.text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(_emoteVault.TryGetEmote)
            .Where(emote => emote != null)
            .Select(emote => emote!)
            .ToArray();

        if (emotes.Length == 0)
            return;

        Yes?.Invoke(new ChatYes(e.username, emotes));
    }
}