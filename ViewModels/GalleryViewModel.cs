using System;
using System.Net.Mime;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Util;

namespace SquarePixel.ViewModels;

public partial class GalleryViewModel: ViewModelBase
{
    public ImageDbViewModel ImageDbViewModel { get; }
    [Reactive] private Bitmap? _selectedBitmap;

    public GalleryViewModel(ImageDbViewModel imageDbViewModel)
    {
        ImageDbViewModel = imageDbViewModel;
        
        this.WhenActivated(disposable =>
        {
            //Loads full res image of selected thumbnail
             ImageDbViewModel.WhenAnyValue(x => x.SelectedImage)
                .Skip(1)
                .Subscribe(idx =>
                {
                    SelectedBitmap?.Dispose();
                    var highRes = ImageDbViewModel.ImageCollection[idx].ImageSource;
                    SelectedBitmap = new Bitmap(highRes.LoadImageFromPath());
                }).DisposeWith(disposable);
             
        });
    }

}