using EventEase.Components;
using EventEase.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton<EventService>();                        // shared data
builder.Services.AddScoped<SessionState>();                           // per-user session
builder.Services.AddSingleton<ICopilotService, RuleBasedCopilot>();   // swap for an LLM-backed implementation

var app = builder.Build();
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error", createScopeForErrors: true);
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
