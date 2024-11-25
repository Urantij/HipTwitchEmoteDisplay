using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemChannel
{
    [JsonConstructor]
    public SemChannel(
        string id,
        string platform,
        string username,
        string displayName,
        long? linkedAt,
        int? emoteCapacity,
        string emoteSetId,
        SemEmoteSet emoteSet
        // User user
    )
    {
        this.Id = id;
        this.Platform = platform;
        this.Username = username;
        this.DisplayName = displayName;
        this.LinkedAt = linkedAt;
        this.EmoteCapacity = emoteCapacity;
        this.EmoteSetId = emoteSetId;
        this.EmoteSet = emoteSet;
        // this.User = user;
    }

    [JsonPropertyName("id")] public string Id { get; }

    [JsonPropertyName("platform")] public string Platform { get; }

    [JsonPropertyName("username")] public string Username { get; }

    [JsonPropertyName("display_name")] public string DisplayName { get; }

    [JsonPropertyName("linked_at")] public long? LinkedAt { get; }

    [JsonPropertyName("emote_capacity")] public int? EmoteCapacity { get; }

    [JsonPropertyName("emote_set_id")] public string EmoteSetId { get; }

    [JsonPropertyName("emote_set")] public SemEmoteSet EmoteSet { get; }

    // [JsonPropertyName("user")] public User User { get; }
}