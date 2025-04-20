using System.Collections.Generic;
using SquarePixel.Models.AI;

namespace SquarePixel.Models;

public class SquareSetting
{
    public IEnumerable<MetaData> MetaData { get; set; }
    public string ImageDataBasePath { get; set; }
    public string InferenceServer { get; set; }
    public int MaxImageWidth { get; set; }
}