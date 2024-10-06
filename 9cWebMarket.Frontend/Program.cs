using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NineCWebMarket.Frontend;
using NineCWebMarket.Frontend.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.Configure<NineChroniclesEndpoints>(builder.Configuration.GetSection(nameof(NineChroniclesEndpoints)));
await builder.Build().RunAsync();