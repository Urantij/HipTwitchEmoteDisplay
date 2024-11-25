namespace HipTwitchEmoteDisplay.Link;

public interface ILinkClient
{
    Task Set(LinkMessage message);
}