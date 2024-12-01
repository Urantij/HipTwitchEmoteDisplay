using Microsoft.Extensions.Options;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetChannelEmotes;
using TwitchLib.Api.Helix.Models.Chat.Emotes.GetGlobalEmotes;

namespace HipTwitchEmoteDisplay.Emotes.Twitch;

public class TwitchEmoter : IEmoter
{
    private readonly TwitchAPI _api;
    private readonly ulong _twitchId;

    public string? EmoteUriTemplate { get; private set; }

    public TwitchEmoter(IOptions<AppConfig> options, IOptions<TwitchApiConfig> apiOptions)
    {
        _twitchId = options.Value.TwitchId;

        _api = new TwitchAPI();
        _api.Settings.ClientId = apiOptions.Value.ClientId;
        _api.Settings.Secret = apiOptions.Value.Secret;
    }

    public async Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default)
    {
        GetGlobalEmotesResponse response = await _api.Helix.Chat.GetGlobalEmotesAsync();

        EmoteUriTemplate = response.Template;

        return response.GlobalEmotes.Select(emote => Create(response.Template, emote)).ToArray();
    }

    public async Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default)
    {
        GetChannelEmotesResponse response = await _api.Helix.Chat.GetChannelEmotesAsync(_twitchId.ToString());

        return response.ChannelEmotes.Select(emote => Create(response.Template, emote)).ToArray();
    }

    private static Emote Create(string template, TwitchLib.Api.Helix.Models.Chat.Emotes.Emote twitchEmote)
    {
        return new Emote(twitchEmote.Name,
            CreateUri(template, twitchEmote.Id, twitchEmote.Format, twitchEmote.Scale, twitchEmote.ThemeMode));
    }

    private static Uri CreateUri(string template, string id, string[] formats, string[] scales, string[] themes)
    {
        // "template": "https://static-cdn.jtvnw.net/emoticons/v2/{{id}}/{{format}}/{{theme_mode}}/{{scale}}"
        // https://static-cdn.jtvnw.net/emoticons/v2/62836/static/light/3.0

        string format = formats.Contains("animated") ? "animated" : formats.First();
        string scale = scales.Last();
        string theme = themes.Contains("dark") ? "dark" : themes.First();

        return new Uri(template
            .Replace("{{id}}", id)
            .Replace("{{format}}", format)
            .Replace("{{theme_mode}}", theme)
            .Replace("{{scale}}", scale)
        );

        // template = template.Replace("{{", "{").Replace("}}", "}");
        //
        // return new Uri(string.Format(template, id, format, theme, scale));
    }
}