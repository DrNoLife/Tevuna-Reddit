using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWASM.Components;

public partial class StatusComponent
{
    [Parameter]
    public string PrimaryStatusText { get; set; } = "Status text here!";

    [Parameter]
    public List<string> StatusMessages { get; set; } = new();

    [Parameter]
    public bool DisplayLoadingStatus { get; set; } = false;

    [Parameter]
    public bool CompactMode { get; set; } = false;

    [Parameter]
    public bool DisplayComponent { get; set; } = true;

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private string _currentMessage = String.Empty;
    private Timer? _timer;
    private bool _loopStarted = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(!_loopStarted)
        {
            await JSRuntime.InvokeVoidAsync("skewImage.applySkew", "skew-image");
        }
    }

    protected override void OnParametersSet()
    {
        if(StatusMessages.Count == 0)
        {
            _currentMessage = PrimaryStatusText;
            return;
        }

        _currentMessage = DisplayLoadingStatus ? PrimaryStatusText : StatusMessages[GetRandomIndex()];
    }

    public void StartMessageLoop(int period)
    {
        _timer = new(UpdateMessage, null, 0, period);
    }

    public void StopMessageLoop() 
    {
        _timer?.Dispose();
    }

    private void UpdateMessage(object? state)
    {
        _loopStarted = true;

        _currentMessage = StatusMessages[GetRandomIndex()];
        StateHasChanged();
    }

    private int GetRandomIndex()
    {
        int randomIndex = new Random().Next(0, StatusMessages.Count - 1);
        return randomIndex;
    }
}
