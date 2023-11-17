using BlazorWASM;
using BlazorWASM.Services;
using BlazorWASM.Services.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress, builder.Configuration);

static void ConfigureServices(IServiceCollection services, string baseAddress, IConfiguration configuration)
{
    string visualApiBaseAddress = configuration["Tevuna:VisualApi"] ?? "http://localddddhost:5000/";
    string biasApiBaseAddress = configuration["Tevuna:BiasApi"] ?? "http://localddddhost:5010/";

    Console.WriteLine($"{nameof(visualApiBaseAddress)} - {visualApiBaseAddress} - {new Uri(visualApiBaseAddress)}");
    Console.WriteLine($"{nameof(biasApiBaseAddress)} - {biasApiBaseAddress} - {new Uri(biasApiBaseAddress)}");

    services.AddHttpClient("Tevuna-Visual", client =>
    {
        client.BaseAddress = new Uri(visualApiBaseAddress);
    });

    services.AddHttpClient("Tevuna-Bias", client =>
    {
        client.BaseAddress = new Uri(biasApiBaseAddress);
    });

    services.AddScoped<IBaseService, BaseService>();
    services.AddScoped<IAnalyserService, AnalyserService>();
    services.AddSingleton<IStateService, StateService>();
}

await builder.Build().RunAsync();
