namespace HipTwitchEmoteDisplay.Emotes;

/// <summary>
/// Интерфейс сервиса, откуда можно брать эмоуты. <see cref="Bttv.BttvEmoter"/> <see cref="Ffz.FfzEmoter"/> <see cref="SemTv.SemTvEmoter"/>
/// </summary>
public interface IEmoter
{
    Task<Emote[]> GetGlobalsAsync(CancellationToken cancellationToken = default);

    Task<Emote[]> GetChannelAsync(CancellationToken cancellationToken = default);
}