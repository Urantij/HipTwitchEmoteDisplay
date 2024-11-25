using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemFile
{
    [JsonConstructor]
    public SemFile(
        string name,
        string staticName,
        int? width,
        int? height,
        int? frameCount,
        int? size,
        string format
    )
    {
        this.Name = name;
        this.StaticName = staticName;
        this.Width = width;
        this.Height = height;
        this.FrameCount = frameCount;
        this.Size = size;
        this.Format = format;
    }

    [JsonPropertyName("name")] public string Name { get; }

    [JsonPropertyName("static_name")] public string StaticName { get; }

    [JsonPropertyName("width")] public int? Width { get; }

    [JsonPropertyName("height")] public int? Height { get; }

    [JsonPropertyName("frame_count")] public int? FrameCount { get; }

    [JsonPropertyName("size")] public int? Size { get; }

    [JsonPropertyName("format")] public string Format { get; }
}