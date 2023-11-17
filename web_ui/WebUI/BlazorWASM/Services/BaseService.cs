using System.Text.Json;
using System.Text;
using BlazorWASM.Services.Abstractions;
using BlazorWASM.Models;

namespace BlazorWASM.Services;

public class BaseService : IBaseService
{
    protected readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<byte[]?> GetByteArrayAsync(string uri, Apis api)
    {
        var httpClient = GetHttpClient(api);
        var response = await httpClient.GetByteArrayAsync(uri);

        return response;

    }

    public async Task<T?> GetAsync<T>(string uri, Apis api)
    {
        var httpClient = GetHttpClient(api);
        var response = await httpClient.GetAsync(uri);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content, Apis api)
    {
        var jsonContent = JsonSerializer.Serialize(content);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var httpClient = GetHttpClient(api);
        var response = await httpClient.PostAsync(uri, httpContent);

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    private HttpClient GetHttpClient(Apis api)
    {
        HttpClient client = api switch
        {
            Apis.VisualApi => _httpClientFactory.CreateClient("Tevuna-Visual"),
            Apis.BiasReportApi => _httpClientFactory.CreateClient("Tevuna-Bias"),
            _ => throw new NotImplementedException()
        };

        return client;
    }
}