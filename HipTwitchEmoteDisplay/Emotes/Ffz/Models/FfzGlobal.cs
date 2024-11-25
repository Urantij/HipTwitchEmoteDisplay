using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Ffz.Models;

public class FfzGlobal
{
    [JsonConstructor]
    public FfzGlobal(
        IReadOnlyList<int> defaultSets,
        IReadOnlyDictionary<string, FfzSet> sets
        // Users users
    )
    {
        this.DefaultSets = defaultSets;
        this.Sets = sets;
        // this.Users = users;
    }

    [JsonPropertyName("default_sets")] public IReadOnlyList<int> DefaultSets { get; }

    [JsonPropertyName("sets")] public IReadOnlyDictionary<string, FfzSet> Sets { get; }

    // [JsonPropertyName("users")] public Users Users { get; }
}