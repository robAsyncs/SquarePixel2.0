using System;
using System.Reactive.Disposables.Fluent;
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
    public LlmViewModel LlmViewModel { get; }

    [Reactive] private ImageItem? _selectedImage;

    [Reactive] private bool _isLoadingImage;
    private DbService _dbService;

    public GalleryViewModel(ImageDbViewModel imageDbViewModel, 
        LlmViewModel llmViewModel,
        DbService inferenceService)
    {
        ImageDbViewModel = imageDbViewModel ?? throw new ArgumentNullException(nameof(imageDbViewModel));
        _dbService = inferenceService ?? throw new ArgumentNullException(nameof(inferenceService));
        LlmViewModel = llmViewModel ?? throw new ArgumentNullException(nameof(llmViewModel));
        
        ImageDbViewModel.WhenAnyValue(x => x.SelectedImage)
            .Skip(1)
            .Where(int.IsPositive)
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
        SelectedImage?.Dispose();
        var selected = ImageDbViewModel.FilteredImages[idx];
        
        await Task.Run(async () =>
        {
            var image = await selected.Photo.FilePath.LoadImageFromPath(1300);
            SelectedImage = new ImageItem(image, selected.Photo);
        }, ct);
    }
}