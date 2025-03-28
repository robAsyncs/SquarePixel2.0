using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Bitmap = System.Drawing.Bitmap;

namespace SquarePixel.Util;

public static class Extensions
{
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
        
        //TODO: LOAD DIRECTLY FROM FILE STREAM
        
        thumbnailBitmap.Save(memory, ImageFormat.Png);
        memory.Position = 0;
        image.Dispose();
        
#pragma warning restore CA1416

        return memory;
    }

    public static Color GenerateAmbientColor(string imagePath)
    {
        return Color.Orange;
    }

    public static Stream AsStream(this Bitmap image)
    {
        var mem = new MemoryStream();
        image.Save(mem, ImageFormat.Bmp);
        mem.Seek(0, SeekOrigin.Begin);
        image.Dispose();
        return mem;
    }
    
}