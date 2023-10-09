using BlazorWASM.Models;

namespace BlazorWASM.Services.Abstractions;

public interface IBaseService
{
    Task<byte[]?> GetByteArrayAsync(string uri, Apis api);
    Task<T?> GetAsync<T>(string uri, Apis api);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content, Apis api);
}