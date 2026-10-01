# EventEase
Blazor Server (.NET 8) event manager with registration and attendance tracking.

## Features
- **EventCard component** (`Components/EventCard.razor`) with two-way binding (`@bind-Value`)
- **Routing**: `/`, `/events/{id}`, `/attendance`, custom Not Found, NavLink menu
- **Validation**: DataAnnotations on events and registration (name, email, capacity, duplicates, full events)
- **Session state**: scoped `SessionState` remembers the current user and their registrations
- **Attendance tracker**: per-event check-in/undo/search plus a cross-event dashboard
- **Copilot helper**: draft description, suggest agenda, live insights (`Services/CopilotService.cs`)

## Run
    dotnet run

Data is in-memory and resets on restart. See `docs/COPILOT_SUMMARY.md` for the development log.
