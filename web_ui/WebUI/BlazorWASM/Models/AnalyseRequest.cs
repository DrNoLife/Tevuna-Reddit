using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorWASM.Models;

public class AnalyseRequest
{
    private string _username = String.Empty;
    [Required(ErrorMessage = "A username is required.")]
    [DisplayName("Reddit Username")]
    public string Username 
    {
        get => _username;
        set
        {
            string valueToUse = value switch
            {
                { } val when val.StartsWith("/u/") => value.Substring(3),
                { } val when val.StartsWith("u/") => value.Substring(2),
                { } val when val.StartsWith("/") => value.Substring(1),
                _ => value
            };

            _username = valueToUse;
        }
    }
}
