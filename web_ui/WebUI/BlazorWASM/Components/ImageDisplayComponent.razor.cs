using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace BlazorWASM.Components;

public partial class ImageDisplayComponent
{
    [Parameter]
    public byte[] ImageBytes { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    private List<string> _imageSources = new();

    protected override async Task OnParametersSetAsync()
    {
        string imageSource =  ImageBytes is not null && ImageBytes.Length > 0 
            ? $"data:image/png;base64,{Convert.ToBase64String(ImageBytes)}" 
            : String.Empty;

        if(!String.IsNullOrEmpty(imageSource))
        {
            if(!_imageSources.Any(x => x.Equals(imageSource)))
            {
                _imageSources.Add(imageSource);
            }
            
            await ScrollToElement("image-section");
        }
    }

    private ValueTask ScrollToElement(string elementId)
    {
        return JSRuntime.InvokeVoidAsync("scrollToElement", elementId);
    }
}
