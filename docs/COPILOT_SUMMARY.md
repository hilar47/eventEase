# How Copilot assisted development

> NOTE: Edit this to reflect what you actually asked GitHub Copilot and what it produced.
> Paste your real prompts and keep screenshots/chat exports in this folder.

| Step | Prompt used (replace with yours) | What Copilot produced | What I fixed / reviewed |
|---|---|---|---|
| 1. Repo & scaffold | "Create a Blazor Server .NET 8 app called EventEase" | Project structure, Program.cs | Verified it builds with `dotnet run` |
| 2. Event Card component | "Create an EventCard Blazor component with Title, Date, Location, Capacity, Description using two-way binding (@bind-Value) and a ValueChanged callback" | `Components/EventCard.razor` with InputText/InputDate/InputNumber and EventCallback | Wired the callback to EditContext.OnFieldChanged; added Dispose to avoid a handler leak |
| 3. Routing | "Add routes for / , /events/{Id:guid}, /attendance and a NotFound page" | `@page` directives, NavLink menu, Router NotFound | Handled missing/deleted events with a friendly message instead of a crash |
| 4. Validation & performance | "Add DataAnnotations validation and reduce unnecessary re-renders" | `[Required]`, `[EmailAddress]`, `[Range]`, `@key` on loops | Replaced manual checks with `EditForm`; capacity/duplicate checks remain server-side in `EventService` |
| 5. Advanced features | "Add a registration form, a scoped session service, and an attendance tracker" | `SessionState`, validated form, `Attendance.razor` | Prefilled form from session; locked button when event is full |
| 6. Documentation | "Summarize the app in a README" | README + this file | Edited for accuracy |
