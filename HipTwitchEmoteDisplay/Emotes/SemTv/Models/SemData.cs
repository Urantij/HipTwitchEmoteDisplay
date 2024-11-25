using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemData
{
    [JsonConstructor]
    public SemData(
        string id,
        string name,
        int? flags,
        int? lifecycle,
        IReadOnlyList<string> state,
        bool? listed,
        bool? animated,
        // Owner owner,
        SemHost host,
        IReadOnlyList<string> tags
    )
    {
        this.Id = id;
        this.Name = name;
        this.Flags = flags;
        this.Lifecycle = lifecycle;
        this.State = state;
        this.Listed = listed;
        this.Animated = animated;
        // this.Owner = owner;
        this.Host = host;
        this.Tags = tags;
    }

    [JsonPropertyName("id")] public string Id { get; }

    [JsonPropertyName("name")] public string Name { get; }

    [JsonPropertyName("flags")] public int? Flags { get; }

    [JsonPropertyName("lifecycle")] public int? Lifecycle { get; }

    [JsonPropertyName("state")] public IReadOnlyList<string> State { get; }

    [JsonPropertyName("listed")] public bool? Listed { get; }

    [JsonPropertyName("animated")] public bool? Animated { get; }

    // [JsonPropertyName("owner")] public Owner Owner { get; }

    [JsonPropertyName("host")] public SemHost Host { get; }

    [JsonPropertyName("tags")] public IReadOnlyList<string> Tags { get; }
}