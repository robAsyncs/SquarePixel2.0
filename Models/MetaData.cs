using System.Collections.Generic;
using System.Drawing;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models.AI;

namespace SquarePixel.Models;

public partial class MetaData: ReactiveObject
{
    public string FilePath { get; set; }
    public IEnumerable<string> Tags { get; set; }
    [Reactive] private string? _imageDescription;
    [Reactive] private Color _ambientColor;
}