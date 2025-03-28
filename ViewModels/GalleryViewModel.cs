using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SquarePixel.Services;
using SquarePixel.Util;

namespace SquarePixel.ViewModels;

public partial class GalleryViewModel : ViewModelBase
{
    public ImageDbViewModel ImageDbViewModel { get; }

    [Reactive] private ImageItem? _selectedBitmap;

    [Reactive] private bool _isLoadingImage;
    private InferenceService _inferenceService;

    public GalleryViewModel(ImageDbViewModel imageDbViewModel, InferenceService inferenceService)
    {
        ImageDbViewModel = imageDbViewModel ?? throw new ArgumentNullException(nameof(imageDbViewModel));
        _inferenceService = inferenceService ?? throw new ArgumentNullException(nameof(inferenceService));
    
        ImageDbViewModel.WhenAnyValue(x => x.SelectedImage)
            .Skip(1)
            .InvokeCommand(LoadHighResImageCommand);
        
        this.WhenActivated(disposable =>
        {
            
            LoadHighResImageCommand.IsExecuting
                .Subscribe(loading => IsLoadingImage = loading)
                .DisposeWith(disposable);
            });
    }


    [ReactiveCommand]
    private async Task LoadHighResImageAsync(int idx, CancellationToken ct)
    {
        SelectedBitmap?.Dispose();
        var highRes = ImageDbViewModel.ImageCollection[idx];

        //todo: stop unnecessary allocations
        var mem = new MemoryStream();
       
        
      
        
        await Task.Run(() =>
        {
            SelectedBitmap = new ImageItem(highRes.ImageSource,
                highRes.ImageSource.LoadImageFromPath(1300));
        });
        
         SelectedBitmap.BitmapThumbnail.Save(mem);
                mem.Seek(0, SeekOrigin.Begin);
                
        var tag = await _inferenceService.PredictImageTag(mem, ct);
        await mem.DisposeAsync();
        SelectedBitmap.MetaData.ImageDescription = tag?.Caption;
    }
    
    
    
    

 
}