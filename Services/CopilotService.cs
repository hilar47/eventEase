using EventEase.Models;

namespace EventEase.Services;

/// <summary>Assistant contract. Implement with Azure OpenAI / Claude API for real LLM output.</summary>
public interface ICopilotService
{
    string DraftDescription(string title, string location);
    List<string> SuggestAgenda(string title);
    List<string> Insights(EventItem ev);
}

/// <summary>Offline, rule-based Copilot so the app runs with no keys.</summary>
public class RuleBasedCopilot : ICopilotService
{
    public string DraftDescription(string title, string location)
    {
        if (string.IsNullOrWhiteSpace(title)) return "Add a title first and I'll draft a description.";
        var where = string.IsNullOrWhiteSpace(location) ? "" : $" at {location}";
        return $"Join us for {title}{where}. Meet fellow attendees, enjoy great sessions, and leave with new ideas and connections. Spots are limited, so register early!";
    }

    public List<string> SuggestAgenda(string title) => new()
    {
        "Doors open & check-in (15 min)",
        $"Welcome & intro to {(string.IsNullOrWhiteSpace(title) ? "the event" : title)} (15 min)",
        "Main session / keynote (45 min)",
        "Networking break (30 min)",
        "Q&A and closing (15 min)"
    };

    public List<string> Insights(EventItem ev)
    {
        var tips = new List<string>();
        var days = (ev.Date - DateTime.Now).TotalDays;
        double fill = ev.Capacity == 0 ? 0 : (double)ev.Registrations.Count / ev.Capacity;

        if (ev.Registrations.Count == 0) tips.Add("No registrations yet. Share the event link and send an announcement.");
        else if (fill >= 0.9) tips.Add($"Almost full ({ev.Registrations.Count}/{ev.Capacity}). Consider a waitlist or raising capacity.");
        else if (fill < 0.3 && days < 7) tips.Add("Under 30% full with under a week to go. Send a reminder push.");

        if (days is > 0 and <= 2) tips.Add("Event is imminent. Email attendees arrival details and a QR check-in code.");
        if (days <= 0 && ev.Registrations.Count > 0)
        {
            tips.Add($"Attendance so far: {ev.AttendanceRate:P0} ({ev.Checked}/{ev.Registrations.Count}).");
            var noShows = ev.Registrations.Count - ev.Checked;
            if (noShows > 0) tips.Add($"{noShows} no-show(s). Send a thank-you to those who came and a 'sorry we missed you' to the rest.");
        }
        if (tips.Count == 0) tips.Add("Everything looks on track.");
        return tips;
    }
}
