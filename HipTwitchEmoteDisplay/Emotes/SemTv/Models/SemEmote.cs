using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemEmote
{
    [JsonConstructor]
    public SemEmote(
        string id,
        string name,
        int? flags,
        // object timestamp,
        string actorId,
        SemData data
        // object originId
    )
    {
        this.Id = id;
        this.Name = name;
        this.Flags = flags;
        // this.Timestamp = timestamp;
        this.ActorId = actorId;
        this.Data = data;
        // this.OriginId = originId;
    }

    [JsonPropertyName("id")] public string Id { get; }

    [JsonPropertyName("name")] public string Name { get; }

    [JsonPropertyName("flags")] public int? Flags { get; }

    // [JsonPropertyName("timestamp")] public object Timestamp { get; }

    [JsonPropertyName("actor_id")] public string ActorId { get; }

    [JsonPropertyName("data")] public SemData Data { get; }

    // [JsonPropertyName("origin_id")] public object OriginId { get; }
}