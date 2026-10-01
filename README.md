# EventEase
Blazor Server (.NET 8) event manager: create events, register attendees, check them in, track attendance.
Copilot features: draft description, suggest agenda, live insights (`Services/CopilotService.cs`).

    dotnet run

Data is in-memory (resets on restart). To use a real LLM, implement `ICopilotService` and register it in Program.cs.
