using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Planner.Client.Services;
using Planner.Domain.Model;
using Planner_Domain.Helpers.HttpHelpers;
using Syncfusion.Blazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<ToDoServices>();
builder.Services.AddScoped<DatePlanServices>();
builder.Services.AddScoped<ActivityServices>();




var ApiUri = builder.Configuration.GetValue<string>("ApiUri");
CustomHttpService.Initialize(builder.Configuration.GetValue<string>("ApiUri"));

await builder.Build().RunAsync();
