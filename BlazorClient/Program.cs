using ApexCharts;
using BlazorClient;
using BlazorClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRadzenComponents();

builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<IAuthClientService, AuthClientService>();
builder.Services.AddScoped<IUserClientService, UserClientService>();
builder.Services.AddScoped<AppStateService>();


builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ClinicVisitAPIUrl"))
});

builder.Services.AddApexCharts();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
