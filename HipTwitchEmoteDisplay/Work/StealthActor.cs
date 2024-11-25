using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Utils;

namespace HipTwitchEmoteDisplay.Work;

public class StealthActor : IHostedService
{
    private readonly LinkGlobal _linkGlobal;
    private readonly ILogger<StealthActor> _logger;

    private bool _hidden = false;

    public StealthActor(LinkGlobal linkGlobal, ILogger<StealthActor> logger)
    {
        _linkGlobal = linkGlobal;
        _logger = logger;
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

        _logger.LogInformation("Прячемся");

        _hidden = true;

        ConsoleHidder.Hide();
    }

    private void LinkGlobalOnNoUsersLeft()
    {
        if (!_hidden)
            return;

        _logger.LogInformation("Вскрываемся");

        _hidden = false;

        ConsoleHidder.Show();
    }
}