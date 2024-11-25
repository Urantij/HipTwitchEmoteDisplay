using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Ffz.Models;

public class FfzSet
{
    [JsonConstructor]
    public FfzSet(
        int id,
        int type,
        // object icon,
        string title,
        // object css,
        IReadOnlyList<FfzEmoticon> emoticons
    )
    {
        this.Id = id;
        this.Type = type;
        // this.Icon = icon;
        this.Title = title;
        // this.Css = css;
        this.Emoticons = emoticons;
    }

    [JsonPropertyName("id")] public int Id { get; }

    [JsonPropertyName("_type")] public int Type { get; }

    // [JsonPropertyName("icon")] public object Icon { get; }

    [JsonPropertyName("title")] public string Title { get; }

    // [JsonPropertyName("css")] public object Css { get; }

    [JsonPropertyName("emoticons")] public IReadOnlyList<FfzEmoticon> Emoticons { get; }
}