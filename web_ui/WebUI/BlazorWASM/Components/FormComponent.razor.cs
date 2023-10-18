using BlazorWASM.Helpers;
using BlazorWASM.Models;
using BlazorWASM.Services.Abstractions;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace BlazorWASM.Components;

public partial class FormComponent
{
    [Parameter]
    public EventCallback<AnalyzedUser> OnImageBytesReceived { get; set; }

    [Inject]
    public IAnalyserService AnalyserService { get; set; }

    [Inject]
    public IStateService StateService { get; set; }

    public bool HasSearchedBefore => StateService.GetOptionValue(Constants.Form.HasSearchedBefore);

    private AnalyseRequest _analyseRequest;
    private bool _errorHappened = false;
    private string _errorMessage = String.Empty;
    private bool _isWorking = false;

    // Regarding the status component.
    private string _status = "Try searching after a Reddit user!";
    private List<string> _statusMessages;
    private bool _showStatusComponent = true;
    private StatusComponent _statusComponent;

    public FormComponent()
    {
        _analyseRequest = new();
        _statusMessages = new()
        {
            "Yoinking all comments & posts from the Reddit API!",
            "It might take some time to get all the information, if the user has a lot of activity...",
            "Retrieving user data from Reddit...",
            "Hang tight, crunching numbers...",
            "Analyzing user activity, please wait...",
            "Fetching comments and posts...",
            "Loading user data...",
            "Pulling information from Reddit...",
            "Please wait while we analyze the user's Reddit activity...",
            "Grabbing the latest data from Reddit...",
            "Hang on, we're fetching the user's Reddit history...",
            "Loading... this may take a minute if the user has a high level of activity."
        };
    }

    private async void HandleValidSubmit()
    {
        try
        {
            _errorHappened = false;
            _errorMessage = String.Empty;
            _showStatusComponent = true;
            _isWorking = true;
            _status = "Retrieving comment and post history.";
            _statusComponent.StartMessageLoop(5000);

            // Start the tasks.
            Task<byte[]?> imageTask = AnalyserService.GetVisualAnalysisAsync(_analyseRequest);
            Task<BiasReport> reportTask = AnalyserService.GetBiasReportAsync(_analyseRequest);

            // Await both tasks to complete.
            await Task.WhenAll(imageTask, reportTask);

            // Get the results.
            byte[]? imageBytes = imageTask.Result;
            BiasReport report = reportTask.Result;

            //byte[]? imageBytes = await AnalyserService.GetVisualAnalysisAsync(_analyseRequest);
            //BiasReport report = await AnalyserService.GetBiasReportAsync(_analyseRequest);

            if (imageBytes is null)
            {
                _errorHappened = true;
                _errorMessage = "For some reason, we failed to retrieve any data from the API.";
                return;
            }

            _status = String.Empty;
            StateService.SetOptionValue(Constants.Form.HasSearchedBefore, true);
            _showStatusComponent = false;

            // Make invokation of the event.
            AnalyzedUser user = new(_analyseRequest.Username, imageBytes, report);
            await OnImageBytesReceived.InvokeAsync(user);
        }
        catch(Exception)
        {
            _errorHappened = true;
            _errorMessage = "Failed to retrieve data from Reddit. \nPlease make sure you entered a valid username.";
            StateService.SetOptionValue(Constants.Form.HasSearchedBefore, false);
            return;
        }
        finally
        {
            _isWorking = false;
            _status = "Try searching after a Reddit user!";
            _statusComponent.StopMessageLoop();
            StateHasChanged();
        }
    }
}
