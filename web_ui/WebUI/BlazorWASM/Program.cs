using BlazorWASM;
using BlazorWASM.Services;
using BlazorWASM.Services.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("RUA-Api", client =>
{
    client.BaseAddress = new Uri("http://127.0.0.1/");
});

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAnalyserService, AnalyserService>();

await builder.Build().RunAsync();
