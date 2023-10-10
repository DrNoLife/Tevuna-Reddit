using BlazorWASM.Models;
using BlazorWASM.Services.Abstractions;

namespace BlazorWASM.Services;

public class AnalyserService : BaseService, IAnalyserService
{
    public AnalyserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<byte[]?> GetVisualAnalysisAsync(AnalyseRequest request)
    {
        return await GetByteArrayAsync($"get-user-activity?username={request.Username}", Apis.VisualApi);
    }

    public async Task<BiasReport> GetBiasReportAsync(AnalyseRequest request)
    {
        return await GetAsync<BiasReport>($"get-user-analysis?username={request.Username}", Apis.BiasReportApi) ?? throw new NullReferenceException();
    }
}
