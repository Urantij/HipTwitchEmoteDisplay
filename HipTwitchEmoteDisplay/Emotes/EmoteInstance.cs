namespace HipTwitchEmoteDisplay.Emotes;

public class EmoteInstance(Emote emote, int start)
{
    public Emote Emote { get; } = emote;
    public int Start { get; } = start;
}