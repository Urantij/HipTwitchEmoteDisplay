using HipTwitchEmoteDisplay.Emotes;
using HipTwitchEmoteDisplay.Link;
using HipTwitchEmoteDisplay.Twitch;
using HipTwitchEmoteDisplay.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace HipTwitchEmoteDisplay.Work;

public class Retranslator : IHostedService
{
    private readonly TwitchChatter _chatter;
    private readonly IHubContext<LinkHub, ILinkClient> _hub;
    private readonly ILogger<Retranslator> _logger;

    private readonly EmoteSelection _emoteSelection;

    public Retranslator(TwitchChatter chatter, IHubContext<LinkHub, ILinkClient> hub, IOptions<AppConfig> options,
        ILogger<Retranslator> logger)
    {
        _emoteSelection = options.Value.EmoteSelection;
        _chatter = chatter;
        _hub = hub;
        _logger = logger;

        _logger.LogInformation("Выбираем смайл {selection}", _emoteSelection);
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
        if (obj.Instances.Length == 0)
            return;

        EmoteInstance? target = _emoteSelection switch
        {
            EmoteSelection.First => obj.Instances[0],
            EmoteSelection.Last => obj.Instances.Last(),
            EmoteSelection.Single when obj.Instances.Length != 1 => null,
            EmoteSelection.Single => obj.Instances[0],
            EmoteSelection.Random => Random.Shared.GetItem(obj.Instances),
            _ => null
        };

        if (target == null)
            return;

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