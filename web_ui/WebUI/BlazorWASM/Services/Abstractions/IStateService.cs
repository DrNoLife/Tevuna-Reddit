namespace BlazorWASM.Services.Abstractions;

public interface IStateService
{
    public bool GetOptionValue(string optionName);
    public void SetOptionValue(string optionName, bool value);
}
