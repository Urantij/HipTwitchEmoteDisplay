using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Emotes.SemTv.Models;

namespace HipTwitchEmoteDisplay.Twitch;

public class ChatYes(string username, Emote[] emotes)
{
    public string Username { get; } = username;
    public Emote[] Emotes { get; } = emotes;
}