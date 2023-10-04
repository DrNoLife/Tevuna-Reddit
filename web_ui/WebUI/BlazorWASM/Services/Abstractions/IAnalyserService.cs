using BlazorWASM.Models;

namespace BlazorWASM.Services.Abstractions;

public interface IAnalyserService
{
    /// <summary>
    /// Makes a request to the analyser API.
    /// </summary>
    /// <param name="request">Request to send to the API.</param>
    /// <returns>A byte array containing the image to be displayed.</returns>
    Task<byte[]?> AnalyseUserAsync(AnalyseRequest request);
}
