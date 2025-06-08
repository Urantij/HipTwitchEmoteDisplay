namespace HipTwitchEmoteDisplay.Emotes;

public class Emote(string key, Uri imageUri, float widthByHeight, bool zeroWidth = false)
{
    public string Key { get; } = key;

    public Uri ImageUri { get; } = imageUri;
    
    /// <summary>
    /// Ширина эмоута поделённая на высоту
    /// </summary>
    public float WidthByHeight { get; } = widthByHeight;

    public bool ZeroWidth { get; } = zeroWidth;
}