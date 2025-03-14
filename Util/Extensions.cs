using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Avalonia.Media.Imaging;
using Bitmap = System.Drawing.Bitmap;

namespace SquarePixel.Util;

public static class Extensions
{
    public static Stream LoadImageFromPath(this string path, int width)
    {
#pragma warning disable CA1416
        
        var image = new Bitmap(path);
        
        var aspectRatio = (float) image.Width / image.Height;
  
        var thumbnailBitmap = new Bitmap(image, new Size(width, (int)(width / aspectRatio)));
        var memory = new MemoryStream();
        thumbnailBitmap.Save(memory, ImageFormat.Png);
        memory.Position = 0;
        image.Dispose();
        
#pragma warning restore CA1416

        return memory;
    }
}