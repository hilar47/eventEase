using EventEase.Models;

namespace EventEase.Services;

public class EventService
{
    private readonly List<EventItem> _events = new();
    private readonly object _lock = new();

    public EventService()
    {
        var e = new EventItem { Title = "Product Launch Night", Location = "Dubai Design District",
            Description = "Be first to see what's next.", Date = DateTime.Today.AddDays(5).AddHours(19), Capacity = 40 };
        e.Registrations.AddRange(new[] {
            new Registration { Name = "Layla Hassan", Email = "layla@example.com", CheckedIn = true, CheckedInAt = DateTime.Now },
            new Registration { Name = "Omar Khan", Email = "omar@example.com" } });
        _events.Add(e);
    }

    public IReadOnlyList<EventItem> GetAll() { lock (_lock) return _events.OrderBy(e => e.Date).ToList(); }
    public EventItem? Get(Guid id) { lock (_lock) return _events.FirstOrDefault(e => e.Id == id); }
    public void Add(EventItem e) { lock (_lock) _events.Add(e); }
    public void Delete(Guid id) { lock (_lock) _events.RemoveAll(e => e.Id == id); }

    public (bool ok, string message) Register(Guid eventId, string name, string email)
    {
        lock (_lock)
        {
            var ev = _events.FirstOrDefault(e => e.Id == eventId);
            if (ev is null) return (false, "Event not found.");
            if (string.IsNullOrWhiteSpace(name) || !email.Contains('@')) return (false, "Enter a name and a valid email.");
            if (ev.Registrations.Count >= ev.Capacity) return (false, "Sorry, this event is full.");
            if (ev.Registrations.Any(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return (false, "That email is already registered.");
            ev.Registrations.Add(new Registration { Name = name.Trim(), Email = email.Trim() });
            return (true, $"{name.Trim()} is registered!");
        }
    }

    public void ToggleCheckIn(Guid eventId, Guid regId)
    {
        lock (_lock)
        {
            var r = _events.FirstOrDefault(e => e.Id == eventId)?.Registrations.FirstOrDefault(x => x.Id == regId);
            if (r is null) return;
            r.CheckedIn = !r.CheckedIn;
            r.CheckedInAt = r.CheckedIn ? DateTime.Now : null;
        }
    }

    public void Cancel(Guid eventId, Guid regId)
    {
        lock (_lock) _events.FirstOrDefault(e => e.Id == eventId)?.Registrations.RemoveAll(r => r.Id == regId);
    }
}
