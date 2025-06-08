using System.Text.Json.Serialization;
using HipTwitchEmoteDisplay.Emotes.SemTv.Models;
using HipTwitchEmoteDisplay.Utils;
using Microsoft.Extensions.Options;

namespace HipTwitchEmoteDisplay.Emotes.SemTv;

[JsonSerializable(typeof(SemChannel))]
public partial class SemTvSerializerContext : JsonSerializerContext
{
}

public class SemTvEmoter : BaseEmoter
{
    // https://gist.github.com/chuckxD/377211b3dd3e8ca8dc505500938555eb?permalink_comment_id=4763229#gistcomment-4763229

    public SemTvEmoter(IOptions<AppConfig> options, ILogger<SemTvEmoter> logger)
        : base(options.Value.TwitchId, LilHelp.MakeProxy(options.Value.Proxy), logger)
    {
    }

    public override async Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default)
    {
        // https://7tv.io/v3/emote-sets/global

        var emoteSet = await GetEmotesAsync<SemEmoteSet>(new Uri("https://7tv.io/v3/emote-sets/global"),
            SemTvSerializerContext.Default,
            cancellationToken);

        return emoteSet.Emotes.Select(Create).ToArray();
    }

    public override async Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default)
    {
        // https://7tv.io/v3/users/twitch/{twitch_id}

        SemChannel channel = await GetEmotesAsync<SemChannel>(new Uri($"https://7tv.io/v3/users/twitch/{_twitchId}"),
            SemTvSerializerContext.Default,
            cancellationToken);

        return channel.EmoteSet.Emotes.Select(Create).ToArray();
    }

    private static Emote Create(SemEmote emote)
    {
        SemFile file = emote.Data.Host.Files
            .Where(f => f.Format == "WEBP")
            .OrderByDescending(f => f.Width)
            .First();

        // //cdn.7tv.app/emote/01EZPHBW3G000C438200A44F1C
        Uri uri = new("https:" + emote.Data.Host.Url + "/" + file.Name);

        float ration;
        if (file is { Width: not null, Height: not null })
            ration = (float)file.Width.Value / (float)file.Height.Value;
        else
            ration = 1;

        return new Emote(emote.Name, uri, ration, zeroWidth: emote.IsZeroWidth());
    }
}