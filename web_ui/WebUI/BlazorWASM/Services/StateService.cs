using BlazorWASM.Services.Abstractions;

namespace BlazorWASM.Services;

public class StateService : IStateService
{
    private Dictionary<string, bool> _boolOptions;

    public StateService()
    {
        _boolOptions = new Dictionary<string, bool>();
    }

    public bool GetOptionValue(string optionName)
    {
        if(!_boolOptions.ContainsKey(optionName))
        {
            return false;
        }

        return _boolOptions[optionName];
    }

    public void SetOptionValue(string optionName, bool value)
    {
        if (_boolOptions.ContainsKey(optionName))
        {
            _boolOptions[optionName] = value;
        }
        else
        {
            _boolOptions.Add(optionName, value);
        }
    }
}
