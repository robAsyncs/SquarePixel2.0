using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace SquarePixel.Models;

public class ImageItem(string filePath, Stream source)
{
    public Bitmap BitmapThumbnail { get; } = new(source);
    public string ImageSource { get; } = filePath;
    public string? ImageClass { get; set; }
    public string? ImageDescription { get; set; }

    

    private async Task<string> GetBorderColorAsync()
    {
        throw new NotImplementedException();

    }
}