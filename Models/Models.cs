namespace EventEase.Models;

public class EventItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today.AddDays(7).AddHours(18);
    public int Capacity { get; set; } = 50;
    public List<Registration> Registrations { get; set; } = new();

    public int Checked => Registrations.Count(r => r.CheckedIn);
    public int SpotsLeft => Math.Max(0, Capacity - Registrations.Count);
    public double AttendanceRate => Registrations.Count == 0 ? 0 : (double)Checked / Registrations.Count;
}

public class Registration
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime RegisteredAt { get; set; } = DateTime.Now;
    public bool CheckedIn { get; set; }
    public DateTime? CheckedInAt { get; set; }
}
