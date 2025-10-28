using System;
using System.Collections.Generic;

namespace SquarePixel.Models.Entities;

public class Photo
{
    public Guid Id { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadedAt { get; set; }
    public string? Caption { get; set; }
    public DateTime? DeletionDate { get; set; }
    public List<string> Tags { get; set; } = [];
}