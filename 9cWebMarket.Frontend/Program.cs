using Blazored.LocalStorage;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NineCWebMarket.Frontend;
using NineCWebMarket.Frontend.Api;
using NineCWebMarket.Frontend.Configuration;
using NineCWebMarket.Frontend.Services;
using NineCWebMarket.Frontend.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.Configure<NineChroniclesEndpoints>(builder.Configuration.GetSection(NineChroniclesEndpoints.SectionName));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IItemNameService, ItemNameService>();
builder.Services.AddSingleton<ITickerNameService, TickerNameService>();
builder.Services.AddSingleton<IApiClient, ApiClient>();
builder.Services.AddSingleton<IMimirClient, MimirClient>();
builder.Services.AddSingleton<IMarketService, MimirService>();
builder.Services.AddSingleton<NineCapiClient>();
builder.Services.AddSingleton<NineCapiService>();

builder.Services.AddTransient<BrowserService>();
builder.Services.AddBlazoredLocalStorage();
var app = builder.Build();
await app.RunAsync();