using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Avalonia.Media.Imaging;
using Bitmap = System.Drawing.Bitmap;

namespace SquarePixel.Util;

public static class Extensions
{
/// <summary>
/// Load image as Stream from a source
/// </summary>
/// <param name="path">Path to image</param>
/// <param name="desiredWidth">Scale width of image whilst maintaining aspect ratio, leave empty if no scaling needed</param>
/// <returns></returns>
/// <exception cref="FileNotFoundException">File not found</exception>
    public static Stream LoadImageFromPath(this string path, int desiredWidth = -1)
    {
#pragma warning disable CA1416

        var memory = new MemoryStream();

        if (!File.Exists(path))
            throw new FileNotFoundException();
        
        var image = new Bitmap(path);
        var aspectRatio = (float) image.Width / image.Height;

        var thumbnailBitmap = desiredWidth != -1 ? 
            new Bitmap(image, new Size(desiredWidth, (int)(desiredWidth / aspectRatio))) : new Bitmap(image);
        
        thumbnailBitmap.Save(memory, ImageFormat.Png);
        memory.Position = 0;
        image.Dispose();
        
#pragma warning restore CA1416

        return memory;
    }
}