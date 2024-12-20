using uasfixnihbanget;
using uasfixnihbanget.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://actbackendseervices.azurewebsites.net/api/") });
builder.Services.AddScoped<Apiservices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<uasfixnihbanget.Components.Pages.Home>()
   .AddInteractiveServerRenderMode();


app.Run();
