namespace HipTwitchEmoteDisplay.Link;

/// <summary>
/// Глобальное хранилище клиентов.
/// </summary>
public class LinkGlobal
{
    private readonly List<string> _users = new();

    public event Action? UserJoined;
    public event Action? NoUsersLeft;

    public void AddUser(string connectionId)
    {
        lock (_users)
            _users.Add(connectionId);

        UserJoined?.Invoke();
    }

    public void RemoveUser(string connectionId)
    {
        bool allGone;

        lock (_users)
        {
            _users.Remove(connectionId);

            allGone = _users.Count == 0;
        }

        if (!allGone)
            return;

        NoUsersLeft?.Invoke();
    }
}