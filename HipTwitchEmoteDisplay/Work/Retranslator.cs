using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Twitch;
using Microsoft.AspNetCore.SignalR;

namespace HipTwitchEmoteDisplay.Work;

public class Retranslator : IHostedService
{
    private readonly TwitchChatter _chatter;
    private readonly IHubContext<LinkHub, ILinkClient> _hub;
    private readonly ILogger<Retranslator> _logger;

    public Retranslator(TwitchChatter chatter, IHubContext<LinkHub, ILinkClient> hub, ILogger<Retranslator> logger)
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
        if (obj.Instances.Length != 1)
            return;

        EmoteInstance target = obj.Instances[0];

        _logger.LogDebug("Отправляем смайл {name}", target.Emote.Key);

        Task.Run(async () =>
        {
            try
            {
                await _hub.Clients.All.Set(new LinkMessage(target.Emote.ImageUri.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        });
    }
}