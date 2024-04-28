using MudBlazor.Services;
using Planner.Server.Components;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Planner.Client.Services;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped(h => new HttpClient() { BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiUri")!) });


builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    option.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<ToDoServices>();
builder.Services.AddScoped<DatePlanServices>();
builder.Services.AddScoped<ActivityServices>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Planner.Client._Imports).Assembly);

app.Run();
