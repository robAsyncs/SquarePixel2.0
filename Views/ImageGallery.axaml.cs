using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class ImageGallery : ReactiveUserControl<GalleryViewModel>
{
    public ImageGallery()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            DataContext = Locator.Current.GetService<GalleryViewModel>();

        });

    }
    
}