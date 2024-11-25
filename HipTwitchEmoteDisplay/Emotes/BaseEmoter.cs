using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes;

public abstract class BaseEmoter : BackgroundService
{
    protected readonly ILogger _logger;

    protected readonly HttpClient _client;

    protected readonly List<Emote> _emoteList = new();

    protected ulong _twitchId;

    protected BaseEmoter(ulong twitchId, IWebProxy? proxy, ILogger logger)
    {
        _logger = logger;
        _twitchId = twitchId;
        _client = new HttpClient(new SocketsHttpHandler()
        {
            Proxy = proxy
        });
    }

    public Emote? TryGetEmote(string key)
    {
        lock (_emoteList)
        {
            return _emoteList.FirstOrDefault(e => e.Key == key);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Emote[] globalSet = await GetGlobalsAsync(stoppingToken);
        lock (_emoteList)
        {
            _emoteList.AddRange(globalSet);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            Emote[] channelSet;
            try
            {
                channelSet = await GetChannelAsync(_twitchId, stoppingToken);
            }
            catch (JsonException)
            {
                _logger.LogCritical("Не удалось пропарсить ответ.");

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                continue;
            }
            catch (Exception e)
            {
                _logger.LogWarning("Не удалось получить ответ: {reason}", e.Message);

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                continue;
            }

            int count;
            lock (_emoteList)
            {
                _emoteList.Clear();
                _emoteList.AddRange(globalSet);
                _emoteList.AddRange(channelSet);

                count = _emoteList.Count;
            }

            _logger.LogDebug("Всосали {count} эмоутов.", count);

            await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="JsonException"></exception>
    protected abstract Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="JsonException"></exception>
    protected abstract Task<Emote[]> GetChannelAsync(ulong channelId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="JsonException"></exception>
    protected async Task<T> GetEmotesAsync<T>(Uri uri, JsonSerializerContext jsonSerializerContext,
        CancellationToken cancellationToken = default)
        where T : class
    {
        using var responseMessage = await _client.GetAsync(uri,
            HttpCompletionOption.ResponseContentRead,
            cancellationToken);

        responseMessage.EnsureSuccessStatusCode();

        T? content =
            await responseMessage.Content.ReadFromJsonAsync(typeof(T), jsonSerializerContext,
                cancellationToken) as T;

        if (content == null)
            throw new JsonException($"не удалось пропарсить ответ. {nameof(GetEmotesAsync)}");

        return content;
    }
}