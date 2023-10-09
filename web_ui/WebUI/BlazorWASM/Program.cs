using BlazorWASM;
using BlazorWASM.Services;
using BlazorWASM.Services.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddHttpClient("RUA-Visual", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5000/");
    });

    services.AddHttpClient("RUA-Bias", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5010/");
    });

    services.AddScoped<IBaseService, BaseService>();
    services.AddScoped<IAnalyserService, AnalyserService>();
    services.AddSingleton<IStateService, StateService>();
}



await builder.Build().RunAsync();
