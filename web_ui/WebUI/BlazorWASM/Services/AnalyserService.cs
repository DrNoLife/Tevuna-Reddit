using BlazorWASM.Models;
using BlazorWASM.Services.Abstractions;

namespace BlazorWASM.Services;

public class AnalyserService : BaseService, IAnalyserService
{
    public AnalyserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<byte[]?> AnalyseUserAsync(AnalyseRequest request)
    {
        return await GetByteArrayAsync($"get-user-activity?username={request.Username}");
    }
}
