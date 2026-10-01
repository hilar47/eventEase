using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public class EventItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Title is required")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Title must be 3-80 characters")]
    public string Title { get; set; } = "";

    [StringLength(500, ErrorMessage = "Max 500 characters")]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Location is required")]
    [StringLength(100)]
    public string Location { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Today.AddDays(7).AddHours(18);

    [Range(1, 10000, ErrorMessage = "Capacity must be 1-10000")]
    public int Capacity { get; set; } = 50;

    public List<Registration> Registrations { get; set; } = new();

    public int Checked => Registrations.Count(r => r.CheckedIn);
    public int SpotsLeft => Math.Max(0, Capacity - Registrations.Count);
    public double AttendanceRate => Registrations.Count == 0 ? 0 : (double)Checked / Registrations.Count;
}

public class RegistrationForm
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Name must be 2-60 characters")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Enter a valid email address")]
    public string Email { get; set; } = "";
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
