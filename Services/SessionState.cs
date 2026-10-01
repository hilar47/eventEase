namespace EventEase.Services;

/// <summary>Per-user (per-circuit) state: remembers who you are and which events you registered for.</summary>
public class SessionState
{
    private readonly HashSet<Guid> _registered = new();
    public string Name { get; private set; } = "";
    public string Email { get; private set; } = "";
    public bool HasProfile => Name != "" && Email != "";
    public event Action? Changed;

    public bool IsRegistered(Guid eventId) => _registered.Contains(eventId);

    public void Remember(string name, string email, Guid eventId)
    {
        Name = name; Email = email; _registered.Add(eventId);
        Changed?.Invoke();
    }

    public void Forget() { Name = ""; Email = ""; _registered.Clear(); Changed?.Invoke(); }
}
