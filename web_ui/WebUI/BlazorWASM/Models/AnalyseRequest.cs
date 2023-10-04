using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorWASM.Models;

public class AnalyseRequest
{
    [Required(ErrorMessage = "A username is required.")]
    [DisplayName("Reddit Username")]
    public string Username { get; set; } = String.Empty;
}
