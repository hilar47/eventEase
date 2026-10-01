using EventEase.Components;
using EventEase.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<ICopilotService, RuleBasedCopilot>(); // swap for an LLM-backed implementation

var app = builder.Build();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
