using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class ImageDbView : ReactiveUserControl<ImageDbViewModel>
{
    public ImageDbView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            DataContext = Locator.Current.GetService<ImageDbViewModel>();
        });

    }
}