using System.Text.Json.Serialization;
using HipTwitchEmoteDisplay.Emotes.Ffz.Models;
using HipTwitchEmoteDisplay.Utils;
using Microsoft.Extensions.Options;

namespace HipTwitchEmoteDisplay.Emotes.Ffz;

[JsonSerializable(typeof(FfzChannel))]
[JsonSerializable(typeof(FfzGlobal))]
public partial class FfzSerializerContext : JsonSerializerContext
{
}

public class FfzEmoter : BaseEmoter
{
    // https://api.frankerfacez.com/v1/room/id/{twitch_id}
    // https://api.frankerfacez.com/v1/set/global

    public FfzEmoter(IOptions<AppConfig> options, ILogger<FfzEmoter> logger)
        : base(options.Value.TwitchId, LilHelp.MakeProxy(options.Value.Proxy), logger)
    {
    }

    protected override async Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default)
    {
        // https://api.frankerfacez.com/v1/set/global

        FfzGlobal global = await GetEmotesAsync<FfzGlobal>(new Uri("https://api.frankerfacez.com/v1/set/global"),
            FfzSerializerContext.Default, cancellationToken);

        return global.Sets.Where(s => global.DefaultSets.Contains(s.Value.Id))
            .SelectMany(s => s.Value.Emoticons)
            .Select(Create)
            .ToArray();
    }

    protected override async Task<Emote[]> GetChannelAsync(ulong channelId,
        CancellationToken cancellationToken = default)
    {
        // https://api.frankerfacez.com/v1/room/id/{twitch_id}

        FfzChannel channel =
            await GetEmotesAsync<FfzChannel>(new Uri($"https://api.frankerfacez.com/v1/room/id/{channelId}"),
                FfzSerializerContext.Default, cancellationToken);

        return channel.Sets.SelectMany(s => s.Value.Emoticons).Select(Create).ToArray();
    }

    private static Emote Create(FfzEmoticon arg)
    {
        return new Emote(arg.Name, arg.Urls.Last().Value);
    }
}