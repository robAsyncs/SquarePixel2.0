using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace SquarePixel.Models;

public class ImageItem(Stream source)
{
    public Bitmap Source { get; } = new(source);
    public string? ImageClass { get; set; }
    public string? ImageDescription { get; set; }

    

    private async Task<string> GetBorderColorAsync()
    {
        throw new NotImplementedException();

    }
}