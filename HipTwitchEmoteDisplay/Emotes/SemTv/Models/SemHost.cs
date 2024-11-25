using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.SemTv.Models;

public class SemHost
{
    [JsonConstructor]
    public SemHost(
        string url,
        IReadOnlyList<SemFile> files
    )
    {
        this.Url = url;
        this.Files = files;
    }

    [JsonPropertyName("url")] public string Url { get; }

    [JsonPropertyName("files")] public IReadOnlyList<SemFile> Files { get; }
}