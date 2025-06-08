namespace HipTwitchEmoteDisplay.Link;

/// <summary>
/// Интерфейс для <see cref="LinkHub"/>
/// </summary>
public interface ILinkClient
{
    Task Set(LinkMessage message);
}