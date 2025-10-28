using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Avalonia;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class GalleryView : ReactiveUserControl<GalleryViewModel>
{
    public GalleryView()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<GalleryViewModel>();
      

    }
}