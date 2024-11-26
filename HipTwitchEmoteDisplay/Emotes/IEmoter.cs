namespace HipTwitchEmoteDisplay.Emotes;

public interface IEmoter
{
    Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default);
    
    Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default);
}