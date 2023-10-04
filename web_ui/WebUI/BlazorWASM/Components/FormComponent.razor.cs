using BlazorWASM.Models;
using BlazorWASM.Services.Abstractions;
using Microsoft.AspNetCore.Components;

namespace BlazorWASM.Components;

public partial class FormComponent
{
    [Parameter]
    public EventCallback<byte[]> OnImageBytesReceived { get; set; }

    [Inject]
    public IAnalyserService AnalyserService { get; set; }

    private AnalyseRequest _analyseRequest;
    private bool _errorHappened = false;
    private string _errorMessage = String.Empty;
    private bool _isWorking = false;
    private string _status = String.Empty;

    public FormComponent()
    {
        _analyseRequest = new();
    }

    private async void HandleValidSubmit()
    {
        _isWorking = true;
        _status = "Retrieving comment and post history.";

        // Make request to API.
        byte[]? imageBytes = await AnalyserService.AnalyseUserAsync(_analyseRequest);

        if(imageBytes is null)
        {
            _errorHappened = true;
            _errorMessage = "For some reason, we failed to retrieve any data from the API.";
            return;
        }

        _isWorking = false;
        _status = String.Empty;

        // Make invokation of the event.
        await OnImageBytesReceived.InvokeAsync(imageBytes);
    }
}
