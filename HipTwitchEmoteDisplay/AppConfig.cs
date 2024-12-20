using HipTwitchEmoteDisplay.Work;

namespace HipTwitchEmoteDisplay;

public class AppConfig
{
    public string TwitchUsername { get; set; }
    public ulong TwitchId { get; set; }

    public string? Proxy { get; set; }

    public EmoteSelection EmoteSelection { get; set; } = EmoteSelection.Last;
}