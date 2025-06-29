using System.Text.Json;
using HipTwitchEmoteDisplay.Emotes.Twitch;

namespace HipTwitchEmoteDisplay.Emotes;

/// <summary>
/// Хранилище всех эмоутов, которые нам известны.
/// </summary>
public class EmoteVault : BackgroundService
{
    class EmoteSet(IEmoter emoter)
    {
        public List<Emote> Emotes { get; } = new();
        public IEmoter Emoter { get; } = emoter;
    }

    private readonly ILogger<EmoteVault> _logger;
    private readonly IEmoter[] _emoters;

    private readonly EmoteSet[] _emoteSets;

    public EmoteVault(IEnumerable<IEmoter> emoters, ILogger<EmoteVault> logger)
    {
        _logger = logger;
        _emoters = emoters.ToArray();
        _emoteSets = _emoters.Select(emoter => new EmoteSet(emoter)).ToArray();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task[] tasks = _emoters.Select(emoter => LoopEmoter(emoter, stoppingToken)).ToArray();

        return Task.WhenAll(tasks);
    }

    public Emote? TryGetEmote(string key)
    {
        Emote? result;

        lock (_emoteSets)
        {
            result = _emoteSets.SelectMany(set => set.Emotes)
                .FirstOrDefault(emote => emote.Key == key);
        }

        return result;
    }

    public Emote MakeEmoteFromId(string key, string id)
    {
        string template = GetEmoteUriTemplate();

        Uri emoteUri = new(template
            .Replace("{{id}}", id)
            .Replace("{{format}}", "default")
            .Replace("{{theme_mode}}", "dark")
            .Replace("{{scale}}", "4.0")
        );

        return new Emote(key, emoteUri, 1);
    }

    /// <summary>
    /// // "template": "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}"
    /// </summary>
    /// <returns></returns>
    private string GetEmoteUriTemplate()
    {
        TwitchEmoter? twitchEmoter = _emoters.OfType<TwitchEmoter>().FirstOrDefault();

        return twitchEmoter?.EmoteUriTemplate ??
               "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}";
    }

    private async Task LoopEmoter(IEmoter emoter, CancellationToken cancellationToken)
    {
        Emote[] globalEmotes;
        try
        {
            globalEmotes = await emoter.GetGlobalsAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Не удалось всосать глобал. {name}", emoter.GetType().Name);
            throw;
        }

        lock (_emoteSets)
        {
            EmoteSet set = _emoteSets.First(set => set.Emoter == emoter);
            set.Emotes.AddRange(globalEmotes);
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            Emote[] channelEmotes;
            try
            {
                channelEmotes = await emoter.GetChannelAsync(cancellationToken);
            }
            catch (JsonException)
            {
                _logger.LogCritical("Не удалось пропарсить ответ.");

                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                continue;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Не удалось получить ответ: {reason}", e.Message);

                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
                continue;
            }

            lock (_emoteSets)
            {
                EmoteSet set = _emoteSets.First(set => set.Emoter == emoter);

                set.Emotes.Clear();
                set.Emotes.AddRange(globalEmotes);
                set.Emotes.AddRange(channelEmotes);
            }

            int count = globalEmotes.Length + channelEmotes.Length;
            _logger.LogDebug("Всосали {count} эмоутов из {name}.", count, emoter.GetType().Name);

            await Task.Delay(TimeSpan.FromMinutes(2), cancellationToken);
        }
    }
}