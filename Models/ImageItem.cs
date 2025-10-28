using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SquarePixel.Models.Entities;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace SquarePixel.Models;

public class ImageItem(Stream source, Photo data): IDisposable
{
    public void Dispose()
    {
        source.Dispose();
        BitmapThumbnail.Dispose();
    }

    public Bitmap BitmapThumbnail { get; } = new(source);
    public Photo Photo { get; } = data;
}