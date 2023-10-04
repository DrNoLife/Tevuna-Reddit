namespace BlazorWASM.Services.Abstractions;

public interface IBaseService
{
    Task<byte[]?> GetByteArrayAsync(string uri);
    Task<T?> GetAsync<T>(string uri);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content);
}