namespace HipTwitchEmoteDisplay.Emotes;

public class EmoteVault
{
    private readonly BaseEmoter[] _emoters;

    public EmoteVault(IEnumerable<BaseEmoter> emoters)
    {
        _emoters = emoters.ToArray();
    }

    public Emote? TryGetEmote(string key)
    {
        Emote? result = null;

        foreach (BaseEmoter emoter in _emoters)
        {
            result = emoter.TryGetEmote(key);

            if (result != null)
                break;
        }

        return result;
    }
}