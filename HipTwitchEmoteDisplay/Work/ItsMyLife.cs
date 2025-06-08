using HipTwitchEmoteDisplay.Link;

namespace HipTwitchEmoteDisplay.Work;

/// <summary>
/// Сервис следит, когда все пользователи отключатся, и через 20 сек всё вырубает, если новых юзеров не появилось.
/// </summary>
public class ItsMyLife : IHostedService
{
    private readonly LinkGlobal _linkGlobal;
    private readonly ILogger<ItsMyLife> _logger;

    private CancellationTokenSource? _deathCts = null;
    private readonly Lock _locker = new();
    
    public ItsMyLife(LinkGlobal linkGlobal, ILogger<ItsMyLife> logger)
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
        lock (_locker)
        {
            if (_deathCts == null)
                return;

            _deathCts.Cancel();
            _deathCts = null;
        }
    }

    private void LinkGlobalOnNoUsersLeft()
    {
        CancellationTokenSource thisCts;
        lock (_locker)
        {
            if (_deathCts != null)
                return;

            thisCts = _deathCts = new CancellationTokenSource();
        }

        Task.Run(async () =>
        {
            try
            {
                _logger.LogInformation("Мы умираем.");
                await Task.Delay(TimeSpan.FromSeconds(20), thisCts.Token);
            }
            catch
            {
                _logger.LogInformation("Мы не умираем.");
                return;
            }

            Environment.Exit(0);
        }, thisCts.Token);
    }
}