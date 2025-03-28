using System.Drawing;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace SquarePixel.Models;

public partial class MetaData: ReactiveObject
{
    [Reactive] private string? _imageClass;
    [Reactive] private string? _imageDescription;
    [Reactive] private Color _ambientColor;
}