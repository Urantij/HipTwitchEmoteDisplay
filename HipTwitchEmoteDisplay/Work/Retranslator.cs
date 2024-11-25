using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Twitch;
using Microsoft.AspNetCore.SignalR;

namespace HipTwitchEmoteDisplay.Work;

public class Retranslator : IHostedService
{
    private readonly TwitchChatter _chatter;
    private readonly IHubContext<LinkHub> _hub;
    private readonly ILogger<Retranslator> _logger;

    public Retranslator(TwitchChatter chatter, IHubContext<LinkHub> hub, ILogger<Retranslator> logger)
    {
        _chatter = chatter;
        _hub = hub;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _chatter.Yes += ChatterOnYes;

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _chatter.Yes -= ChatterOnYes;

        return Task.CompletedTask;
    }

    private void ChatterOnYes(ChatYes obj)
    {
        if (obj.Emotes.Length != 1)
            return;

        Emote target = obj.Emotes[0];

        _logger.LogDebug("Отправляем смайл {name}", target.Key);

        Task.Run(async () =>
        {
            try
            {
                await _hub.Clients.All.SendAsync("Set", new LinkMessage(target.ImageUri.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }
}