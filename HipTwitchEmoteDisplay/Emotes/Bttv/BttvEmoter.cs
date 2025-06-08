using System.Text.Json.Serialization;
using HipTwitchEmoteDisplay.Emotes.Bttv.Models;
using HipTwitchEmoteDisplay.Utils;
using Microsoft.Extensions.Options;

namespace HipTwitchEmoteDisplay.Emotes.Bttv;

[JsonSerializable(typeof(BttvEmote[]))]
[JsonSerializable(typeof(BttvChannel))]
public partial class BttvSerializerContext : JsonSerializerContext
{
}

public class BttvEmoter : BaseEmoter
{
    public BttvEmoter(IOptions<AppConfig> options, ILogger<BttvEmoter> logger)
        : base(options.Value.TwitchId, LilHelp.MakeProxy(options.Value.Proxy), logger)
    {
    }

    public override async Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default)
    {
        // https://api.betterttv.net/3/cached/emotes/global

        BttvEmote[] global = await GetEmotesAsync<BttvEmote[]>(
            new Uri("https://api.betterttv.net/3/cached/emotes/global"),
            BttvSerializerContext.Default, cancellationToken);

        return global.Select(Create).ToArray();
    }

    public override async Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default)
    {
        // https://api.betterttv.net/3/cached/users/twitch/{twitchid}

        BttvChannel channel =
            await GetEmotesAsync<BttvChannel>(new Uri($"https://api.betterttv.net/3/cached/users/twitch/{_twitchId}"),
                BttvSerializerContext.Default, cancellationToken);

        return channel.ChannelEmotes
            .Concat(channel.SharedEmotes)
            .Select(Create)
            .ToArray();
    }

    private Emote Create(BttvEmote arg)
    {
        // https://cdn.betterttv.net/emote/5a970ab2122e4331029f0d7e/3x

        float ration;
        if (arg is { Width: not null, Height: not null })
            ration = (float)arg.Width.Value / (float)arg.Height.Value;
        else
            ration = 1;

        return new Emote(arg.Code, new Uri($"https://cdn.betterttv.net/emote/{arg.Id}/3x"), ration);
    }
}