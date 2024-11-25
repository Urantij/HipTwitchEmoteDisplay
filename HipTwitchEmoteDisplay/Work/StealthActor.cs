using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Utils;

namespace HipTwitchEmoteDisplay.Work;

public class StealthActor : IHostedService
{
    private readonly LinkGlobal _linkGlobal;

    private bool _hidden = false;

    public StealthActor(LinkGlobal linkGlobal)
    {
        _linkGlobal = linkGlobal;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _linkGlobal.UserJoined += LinkGlobalOnUserJoined;
        _linkGlobal.NoUsersLeft += LinkGlobalOnNoUsersLeft;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _linkGlobal.UserJoined -= LinkGlobalOnUserJoined;
        _linkGlobal.NoUsersLeft -= LinkGlobalOnNoUsersLeft;

        return Task.CompletedTask;
    }

    private void LinkGlobalOnUserJoined()
    {
        if (_hidden)
            return;

        _hidden = true;

        ConsoleHidder.Hide();
    }

    private void LinkGlobalOnNoUsersLeft()
    {
        if (!_hidden)
            return;
        
        _hidden = false;

        ConsoleHidder.Show();
    }
}