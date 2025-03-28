using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class GalleryView : ReactiveUserControl<GalleryViewModel>
{
    public GalleryView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            DataContext = Locator.Current.GetService<GalleryViewModel>();
        });

    }
}