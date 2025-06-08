namespace HipTwitchEmoteDisplay.Emotes;

/// <summary>
/// Конкретный эмоут, который может включать себя в несколько эмоутов, если они накладываемые. 
/// </summary>
public class EmoteInstance(Emote[] emotes)
{
    public Emote[] Emotes { get; } = emotes;
}