using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SquarePixel.Services;
using SquarePixel.Util;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class ImageDbViewModel : ViewModelBase
{
    public SourceList<ImageItem> ImageCollection { get; } = new();
    public ReadOnlyObservableCollection<ImageItem> FilteredImages => _filteredImages;
    private readonly ReadOnlyObservableCollection<ImageItem> _filteredImages;
    private ISukiDialogManager _dialogManager { get; }
    public ObservableCollection<string> ImageClasses { get; } = ["All"];
    [Reactive] private int _selectedImage;
    [Reactive] private int _filterByClassValue;
    private SettingService<SquareSetting> _settingService;
    private InferenceService _inferenceService;

    public ImageDbViewModel(
        ISukiDialogManager dialogManager,
        SettingService<SquareSetting> settingService,
        InferenceService inferenceService)
    {
        _dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
        _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
        _inferenceService = inferenceService ?? throw new ArgumentNullException(nameof(inferenceService));

        ImageCollection.Connect()
            .AutoRefreshOnObservable(x =>
                this.WhenAnyValue(x => x.FilterByClassValue))
            .Filter(x => x.MetaData.Tags.Contains(ImageClasses[FilterByClassValue]))
            .Bind(out _filteredImages)
            .Subscribe();

        this.WhenActivated(disposable =>
        {
            //todo: reaplce with setting directory
            LoadGalleryFolderCommand
                .Execute(@"C:\\Users\\robel\\Desktop\\OneDrive\\Gallery\\Shared Gallery Folder\\Mk Share")
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe()
                .DisposeWith(disposable);

            LoadGalleryFolderCommand.ThrownExceptions.Subscribe();
        });
    }

    [ReactiveCommand]
    private async Task LoadGalleryFolder(string? dirName, CancellationToken ct)
    {
        ImageCollection.Clear();
        IEnumerable<string> dir;
        try
        {
            if (string.IsNullOrWhiteSpace(dirName))
                return;

            dir = new DirectoryInfo(dirName).GetFiles("*.jpg")
                .Select(x => x.FullName);

        }
        catch (Exception ex)
        {
            _dialogManager.Popup(NotificationType.Error, "Operation Failed", ex.Message);
            return;
        }

        await Task.Run(async () =>
        {
            await foreach (var item in GenerateImageItemsAsync(dir, ct))
                ImageCollection.Add(item);
        }, ct);

    }

    private async IAsyncEnumerable<ImageItem> GenerateImageItemsAsync(
        IEnumerable<string> dir, 
        [EnumeratorCancellation] CancellationToken ct)
    {
        foreach (var imagePath in dir)
        {
            var imageStream = await imagePath.LoadImageFromPath(desiredWidth: 350);
            var imageClassifications = await imagePath.TryLoadTagsFromDisk() ??
                                       await _inferenceService.GenerateImageTags(imageStream, imagePath, ct);
            foreach (var className in imageClassifications)
                if (!ImageClasses.Contains(className)) 
                    ImageClasses.Add(className);

            var caption = await _inferenceService.GenerateImageCaption(imageStream, ct);
            var ambiance = await Extensions.GenerateAmbientColor(imageStream, ct);

            yield return new ImageItem(imageStream,
                new MetaData
                {
                    FilePath = imagePath,
                    Tags = imageClassifications,
                    ImageDescription = caption?.Caption,
                    AmbientColor = ambiance
                });

            await imageStream.DisposeAsync();
        }
    }
}