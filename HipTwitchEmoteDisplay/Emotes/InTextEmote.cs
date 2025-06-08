namespace HipTwitchEmoteDisplay.Emotes;

/// <summary>
/// Определённый эмоут из сообщения и его позиция в тексте.
/// </summary>
/// <param name="emote"></param>
/// <param name="start"></param>
public class InTextEmote(Emote emote, int start)
{
    public Emote Emote { get; } = emote;
    public int Start { get; } = start;
}