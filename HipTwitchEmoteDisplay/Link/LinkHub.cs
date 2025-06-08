using Microsoft.AspNetCore.SignalR;

namespace HipTwitchEmoteDisplay.Link;

/// <summary>
/// Хаб, который ловит сообщения клиентов.
/// </summary>
public class LinkHub : Hub<ILinkClient>
{
    private readonly LinkGlobal _global;
    private readonly ILogger<LinkHub> _logger;

    public LinkHub(LinkGlobal global, ILogger<LinkHub> logger)
    {
        _global = global;
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Есть коннект.");

        _global.AddUser(Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Нет коннекта.");

        _global.RemoveUser(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}