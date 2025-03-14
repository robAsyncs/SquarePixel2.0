using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class GalleryView : ReactiveUserControl<GalleryViewModel>
{
    public GalleryView()
    {
        InitializeComponent();
    }
}