namespace BlazorWASM.Models;

public class BiasReport
{
    public string Username { get; set; }
    public string Analysis { get; set; }
    public List<string> AnalysisParagraphs 
    { 
        get
        {
            var splitAnalysis = Analysis.Split('\n');
            var nonEmptyRows = splitAnalysis.Where(x => !String.IsNullOrEmpty(x)).ToList();
            return nonEmptyRows;
        }
    }

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
