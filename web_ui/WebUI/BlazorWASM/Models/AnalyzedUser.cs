namespace BlazorWASM.Models;

public class AnalyzedUser
{
    public string Username { get; set; }
    public byte[] ImageBytes { get; set; }
    public BiasReport BiasReport { get; set; }

    public AnalyzedUser(string username, byte[] imageBytes, BiasReport biasReport)
    {
        Username = username;
        ImageBytes = imageBytes;
        BiasReport = biasReport;
    }

    public string GetImageUrl() => $"data:image/png;base64,{Convert.ToBase64String(ImageBytes)}";
}
