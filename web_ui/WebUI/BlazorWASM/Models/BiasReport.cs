namespace BlazorWASM.Models;

public class BiasReport
{
    public string Username { get; set; }
    public string Analysis { get; set; }

    public BiasReport()
    {
        Username = String.Empty;
        Analysis = String.Empty;
    }

    public BiasReport(string username, string analysis)
    {
        Username = username;
        Analysis = analysis;
    }
}
