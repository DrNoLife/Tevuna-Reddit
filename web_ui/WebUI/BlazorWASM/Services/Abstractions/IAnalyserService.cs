using BlazorWASM.Models;

namespace BlazorWASM.Services.Abstractions;

public interface IAnalyserService
{
    /// <summary>
    /// Makes a request to the visual analyser API.
    /// </summary>
    /// <param name="request">Request to send to the API.</param>
    /// <returns>A byte array containing the image to be displayed.</returns>
    Task<byte[]?> GetVisualAnalysisAsync(AnalyseRequest request);

    /// <summary>
    /// Makes a request to the bias report API.
    /// </summary>
    /// <returns>A BiasReport object containing the analysis of the user.</returns>
    Task<BiasReport> GetBiasReportAsync(AnalyseRequest request);
}
