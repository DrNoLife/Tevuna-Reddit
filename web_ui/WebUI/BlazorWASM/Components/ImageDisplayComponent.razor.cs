using BlazorWASM.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWASM.Components;

public partial class ImageDisplayComponent
{
    [Parameter]
    public List<AnalyzedUser>? AnalyzedUsers { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await ScrollToElement("image-section");
    }

    private ValueTask ScrollToElement(string elementId)
    {
        return JSRuntime.InvokeVoidAsync("scrollToElement", elementId);
    }
}
