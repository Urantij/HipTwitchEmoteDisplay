using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Ffz.Models;

public class FfzEmoticon
{
    [JsonConstructor]
    public FfzEmoticon(
        int id,
        string name,
        int height,
        int width,
        bool @public,
        bool hidden,
        bool modifier,
        int modifierFlags,
        // object offset,
        // object margins,
        // object css,
        // Owner owner,
        // object artist,
        IReadOnlyDictionary<string, Uri> urls
        // int status,
        // int usageCount,
        // DateTime createdAt,
        // DateTime lastUpdated
    )
    {
        this.Id = id;
        this.Name = name;
        this.Height = height;
        this.Width = width;
        this.Public = @public;
        this.Hidden = hidden;
        this.Modifier = modifier;
        this.ModifierFlags = modifierFlags;
        // this.Offset = offset;
        // this.Margins = margins;
        // this.Css = css;
        // this.Owner = owner;
        // this.Artist = artist;
        this.Urls = urls;
        // this.Status = status;
        // this.UsageCount = usageCount;
        // this.CreatedAt = createdAt;
        // this.LastUpdated = lastUpdated;
    }

    [JsonPropertyName("id")] public int Id { get; }

    [JsonPropertyName("name")] public string Name { get; }

    [JsonPropertyName("height")] public int Height { get; }

    [JsonPropertyName("width")] public int Width { get; }

    [JsonPropertyName("public")] public bool Public { get; }

    [JsonPropertyName("hidden")] public bool Hidden { get; }

    [JsonPropertyName("modifier")] public bool Modifier { get; }

    [JsonPropertyName("modifier_flags")] public int ModifierFlags { get; }

    // [JsonPropertyName("offset")] public object Offset { get; }

    // [JsonPropertyName("margins")] public object Margins { get; }

    // [JsonPropertyName("css")] public object Css { get; }

    // [JsonPropertyName("owner")] public Owner Owner { get; }

    // [JsonPropertyName("artist")] public object Artist { get; }

    [JsonPropertyName("urls")] public IReadOnlyDictionary<string, Uri> Urls { get; }

    // [JsonPropertyName("status")] public int Status { get; }

    // [JsonPropertyName("usage_count")] public int UsageCount { get; }

    // [JsonPropertyName("created_at")] public DateTime CreatedAt { get; }

    // [JsonPropertyName("last_updated")] public DateTime LastUpdated { get; }
}