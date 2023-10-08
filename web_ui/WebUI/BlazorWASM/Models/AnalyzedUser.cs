namespace BlazorWASM.Models;

public class AnalyzedUser
{
    public string Username { get; set; }
    public byte[] ImageBytes { get; set; }

    public AnalyzedUser(string username, byte[] imageBytes)
    {
        Username = username;
        ImageBytes = imageBytes;
    }

    public string GetImageUrl() => $"data:image/png;base64,{Convert.ToBase64String(ImageBytes)}";
}
