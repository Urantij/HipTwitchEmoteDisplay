using System.Text.Json;

namespace HipTwitchEmoteDisplay.Emotes;

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