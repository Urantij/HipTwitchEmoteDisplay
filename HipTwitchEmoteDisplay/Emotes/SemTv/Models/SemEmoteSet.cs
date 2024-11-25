using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemEmoteSet
{
    [JsonConstructor]
    public SemEmoteSet(
        string id,
        string name,
        int? flags,
        // IReadOnlyList<object> tags,
        bool? immutable,
        bool? privileged,
        IReadOnlyList<SemEmote> emotes,
        int? emoteCount,
        int? capacity
        // object owner
    )
    {
        this.Id = id;
        this.Name = name;
        this.Flags = flags;
        // this.Tags = tags;
        this.Immutable = immutable;
        this.Privileged = privileged;
        this.Emotes = emotes;
        this.EmoteCount = emoteCount;
        this.Capacity = capacity;
        // this.Owner = owner;
    }

    [JsonPropertyName("id")] public string Id { get; }

    [JsonPropertyName("name")] public string Name { get; }

    [JsonPropertyName("flags")] public int? Flags { get; }

    // [JsonPropertyName("tags")] public IReadOnlyList<object> Tags { get; }

    [JsonPropertyName("immutable")] public bool? Immutable { get; }

    [JsonPropertyName("privileged")] public bool? Privileged { get; }

    [JsonPropertyName("emotes")] public IReadOnlyList<SemEmote> Emotes { get; }

    [JsonPropertyName("emote_count")] public int? EmoteCount { get; }

    [JsonPropertyName("capacity")] public int? Capacity { get; }

    // [JsonPropertyName("owner")] public object Owner { get; }
}