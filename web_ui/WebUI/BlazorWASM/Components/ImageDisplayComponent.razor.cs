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

    private AnalyzedUser? _selectedUser;

    public ImageDisplayComponent()
    {
        _selectedUser = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await ScrollToElement("image-section");
    }

    private List<AnalyzedUser> GetAnalyzedUsersReversed()
    {
        var newList = AnalyzedUsers.ToList();
        newList.Reverse();

        return newList;
    }

    private ValueTask ScrollToElement(string elementId)
    {
        return JSRuntime.InvokeVoidAsync("scrollToElement", elementId);
    }

    private void HandleUserClicked(AnalyzedUser user)
    {
        _selectedUser = user;
    }
}
