using HipTwitchEmoteDisplay.Emotes;

namespace HipTwitchEmoteDisplay.Twitch;

public class ChatYes(string username, EmoteInstance[] instances)
{
    public string Username { get; } = username;
    public EmoteInstance[] Instances { get; } = instances;
}