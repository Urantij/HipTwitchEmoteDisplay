using System.Diagnostics;

namespace HipTwitchEmoteDisplay.Work;

/// <summary>
/// Сервис после запуска приложения пытается открыть страницу.
/// Получается не всегда, надо сказать.
/// </summary>
public class PageOpener : IHostedService
{
    private readonly IHostApplicationLifetime _lifetime;
    private readonly Uri _url;

    public PageOpener(IHostApplicationLifetime lifetime, IConfiguration configuration)
    {
        _lifetime = lifetime;
        _url = new Uri(new Uri(configuration["urls"]), "Index.html");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _lifetime.ApplicationStarted.Register(Started);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void Started()
    {
        Process.Start(new ProcessStartInfo(_url.ToString()) { UseShellExecute = true });
    }
}