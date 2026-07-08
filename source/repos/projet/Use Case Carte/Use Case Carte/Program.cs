using Use_Case_Carte.Components;
using Use_Case_Carte.Services;
using Blazored.LocalStorage;
using Use_Case_Carte.Components.Route;
using Use_Case_Carte.Components.Layout;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Enregistrer les services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NavigationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ProfilService>();
builder.Services.AddScoped<SafeJs>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5229/") });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
