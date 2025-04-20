using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Kernel;
using SquarePixel.Models;
using SquarePixel.Models.AI;
using Bitmap = System.Drawing.Bitmap;

namespace SquarePixel.Util;

public static class Extensions
{
    public static async Task<MemoryStream> LoadImageFromPath(this string path, int desiredWidth = -1)
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
        thumbnailBitmap.Dispose();
        
#pragma warning restore CA1416

        return memory;
    }

    public static async Task<Color> GenerateAmbientColor(MemoryStream stream, CancellationToken ct)
    {
        return Color.Transparent;
    }

    public static Stream AsStream(this Bitmap image)
    {
        var mem = new MemoryStream();
        image.Save(mem, ImageFormat.Bmp);
        mem.Seek(0, SeekOrigin.Begin);
        image.Dispose();
        return mem;
    }

    private static string[][] Tags = [["Car", "Plane", "Jet"], ["Space", "Sky"], 
        ["Computer", "Phone"], ["Ring", "People"], ["Ring", "People"]];
    public static async Task<string[]?> TryLoadTagsFromDisk(this string filePath)
    {
        var tags = Tags[Random.Shared.NextInt64(0, Tags.Length - 1)].ToList();
        return tags.Append("All").ToArray();
    }
    
    public static string[] GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        return ["All",
            new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()), 
            new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()),
            new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray())
        ];
    }
    
}