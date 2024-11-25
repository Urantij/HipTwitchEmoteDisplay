using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Bttv.Models;

public class BttvChannel
{
    [JsonConstructor]
    public BttvChannel(
        string id,
        // List<object> bots,
        // string avatar,
        IReadOnlyList<BttvEmote> channelEmotes,
        IReadOnlyList<BttvEmote> sharedEmotes
    )
    {
        this.Id = id;
        // this.Bots = bots;
        // this.Avatar = avatar;
        this.ChannelEmotes = channelEmotes;
        this.SharedEmotes = sharedEmotes;
    }

    [JsonPropertyName("id")] public string Id { get; }

    // [JsonPropertyName("bots")] public IReadOnlyList<object> Bots { get; }

    // [JsonPropertyName("avatar")] public string Avatar { get; }

    [JsonPropertyName("channelEmotes")] public IReadOnlyList<BttvEmote> ChannelEmotes { get; }

    [JsonPropertyName("sharedEmotes")] public IReadOnlyList<BttvEmote> SharedEmotes { get; }
}