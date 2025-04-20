using System;
using System.IO;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace SquarePixel.Models;

public class ImageItem(Stream source, MetaData data): IDisposable
{
    public void Dispose()
    {
        source.Dispose();
        BitmapThumbnail.Dispose();
    }

    public Bitmap BitmapThumbnail { get; } = new(source);
    public MetaData MetaData { get; } = data;
}