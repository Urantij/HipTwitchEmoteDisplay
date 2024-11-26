using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes;

public abstract class BaseEmoter : IEmoter
{
    protected readonly ILogger _logger;

    protected readonly HttpClient _client;

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

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="JsonException"></exception>
    public abstract Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="TaskCanceledException"></exception>
    /// <exception cref="JsonException"></exception>
    public abstract Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default);

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