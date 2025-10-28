using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using DynamicData;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SquarePixel.Models.Entities;
using SquarePixel.Services;
using SquarePixel.Util;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class ImageDbViewModel : ViewModelBase
{
    public ObservableCollection<ImageItem> ImageCollection { get; } = [];
    public ReadOnlyObservableCollection<ImageItem> FilteredImages => _filteredImages;
    private readonly ReadOnlyObservableCollection<ImageItem> _filteredImages;
    private ISukiDialogManager _dialogManager { get; }
    public ObservableCollection<string> ImageClasses { get; } = ["All"];
    public IReadOnlyList<IStorageFile> Paths { get; set; }

    public Interaction<Unit, IReadOnlyList<IStorageFile>> PickFileInteraction { get; } = new();

    [Reactive] private int _selectedImage;
    [Reactive] private int _filterByClassValue;
    private SettingService<SquareSetting> _settingService;
    private DbService _dbService;
    private RabbitMqService _rabbitMqService;

    public ImageDbViewModel(
        ISukiDialogManager dialogManager,
        SettingService<SquareSetting> settingService,
        RabbitMqService rabbitMqService,
        DbService dbService)
    {
        _rabbitMqService = rabbitMqService ?? throw new ArgumentNullException(nameof(rabbitMqService));
        _dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
        _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
        _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));

        // ImageCollection.Connect()
        //     .AutoRefreshOnObservable(x =>
        //         this.WhenAnyValue(x => x.FilterByClassValue))
        //     .Filter(x => x.Photo.Tags.Contains(ImageClasses[FilterByClassValue]))
        //     .Bind(out _filteredImages)
        //     .Subscribe();

        this.WhenActivated(disposable =>
        {
            //todo: REPLACE with setting directory
            
            Observable.StartAsync(async ct => await LoadGalleryAsync(ct));
            LoadGalleryCommand.ThrownExceptions.Subscribe().DisposeWith(disposable);
        });
    }

    [ReactiveCommand]
    private async Task LoadGalleryAsync(CancellationToken ct)
    {
        ImageCollection.Clear();
        
        await Task.Run(async () =>
        {
            await foreach (var item in LoadImagesAsync(ct))
                ImageCollection.Add(item);
        }, ct);

    }

    private async IAsyncEnumerable<ImageItem> LoadImagesAsync([EnumeratorCancellation] CancellationToken ct)
    {
        var images = await _dbService.RetrieveImagesAsync(ct);
        foreach (var image in images)
        {
            //Load images from disk and captions from db, then 
            using var imageStream = await image.FilePath.LoadImageFromPath(desiredWidth: 350);
            
            foreach (var className in image.Tags)
                if (!ImageClasses.Contains(className)) 
                    ImageClasses.Add(className);
            
            yield return new ImageItem(imageStream, image);
            // await imageStream.DisposeAsync();
        }
    }
    
    [ReactiveCommand] private async Task PickFileAsync(CancellationToken ct)
    {
        var photos = (await PickFileInteraction.Handle(Unit.Default))
            .Select(x => new Photo
        {
            Id = new Guid(),
            FilePath = x.Path.LocalPath,
            UploadedAt = DateTime.Now.ToUniversalTime(),
        }).ToArray();
            
        await _dbService.SaveUniqueImagesAsync(photos, ct);
        await _rabbitMqService.PublishImageAsync(photos, ct);
    }
}