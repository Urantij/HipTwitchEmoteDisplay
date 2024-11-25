using System.Text.Json.Serialization;

namespace HipTwitchEmoteDisplay.Emotes.Bttv.Models;

public class BttvEmote
{
    [JsonConstructor]
    public BttvEmote(
        string id,
        string code,
        string imageType,
        bool animated,
        // User user,
        int? width,
        int? height
    )
    {
        this.Id = id;
        this.Code = code;
        this.ImageType = imageType;
        this.Animated = animated;
        // this.User = user;
        this.Width = width;
        this.Height = height;
    }

    [JsonPropertyName("id")] public string Id { get; }

    [JsonPropertyName("code")] public string Code { get; }

    [JsonPropertyName("imageType")] public string ImageType { get; }

    [JsonPropertyName("animated")] public bool Animated { get; }

    // [JsonPropertyName("user")] public User User { get; }

    [JsonPropertyName("width")] public int? Width { get; }

    [JsonPropertyName("height")] public int? Height { get; }
}