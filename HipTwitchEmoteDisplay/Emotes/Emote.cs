namespace HipTwitchEmoteDisplay.Emotes;

public class Emote(string key, Uri imageUri)
{
    public string Key { get; } = key;

    public Uri ImageUri { get; } = imageUri;
}