namespace HipTwitchEmoteDisplay.Link;

public class LinkMessage(DisplayEmoteInfo[] infos)
{
    public DisplayEmoteInfo[] Infos { get; } = infos;
}

public class DisplayEmoteInfo(string uri, float wbh)
{
    public string Uri { get; } = uri;
    /// <summary>
    /// Процент отображаемой высоты. [0-1]
    /// </summary>
    public float Wbh { get; } = wbh;
}