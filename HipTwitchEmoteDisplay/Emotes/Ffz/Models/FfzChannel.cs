using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Ffz.Models;

public class FfzChannel
{
    [JsonConstructor]
    public FfzChannel(
        IReadOnlyDictionary<string, FfzSet> sets
    )
    {
        this.Sets = sets;
    }

    [JsonPropertyName("sets")] public IReadOnlyDictionary<string, FfzSet> Sets { get; }
}