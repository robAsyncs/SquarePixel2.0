using System;
using System.IO;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace SquarePixel.Models;

public class ImageItem(string filePath, Stream source): IDisposable
{
    public void Dispose()
    {
        source.Dispose();
        BitmapThumbnail.Dispose();
    }

    public Bitmap BitmapThumbnail { get; } = new(source);
    public string ImageSource { get; } = filePath;
    public MetaData MetaData { get; set; } = new();
}