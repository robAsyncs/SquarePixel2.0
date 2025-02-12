namespace SquarePixel.Models;

public class ImageItem(string Source)
{
    public string Source { get; set; } = Source;
    public string? ImageClass { get; set; }
    public string? ImageDescription { get; set; }
}