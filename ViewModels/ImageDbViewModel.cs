using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SquarePixel.Services;
using SquarePixel.Util;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class ImageDbViewModel: ViewModelBase
{
    //replace with dynamic data for pagination and filtering
    public ObservableCollection<ImageItem> ImageCollection { get; } = [];
    private ISukiDialogManager _dialogManager { get; }
    
    //todo: replace with dynamic data coll
    public ObservableCollection<string> ImageClasses { get; } = ["--","Planes", "Buildings"];
    [Reactive] private int _selectedImage;
    [Reactive] private int _filterByClassValue;
    private SettingService<Setting> _settingService;
    private InferenceService _inferenceService;
    public ImageDbViewModel(
        ISukiDialogManager dialogManager, 
        SettingService<Setting> settingService,
        InferenceService inferenceService)
    {
        _dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
        _settingService = settingService ?? throw new ArgumentNullException(nameof(settingService));
        _inferenceService = inferenceService ?? throw new ArgumentNullException(nameof(inferenceService));
        
        this.WhenActivated(disposable =>
        {
            this.WhenAnyValue(x => x.FilterByClassValue)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(FilterBySelectedClassCommand).DisposeWith(disposable);
            
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
    private void FilterBySelectedClass(int classPosition)
    {
        
    }
    
    
    
    [ReactiveCommand] private async Task LoadGalleryFolder(string? dirName, CancellationToken ct)
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
            _dialogManager.Popup(NotificationType.Error, "Operation Failed",ex.Message);
            return;
        }

        //todo: replace with iasyncenumerable
        await Task.Run(async () =>
        {
            foreach (var imagePath in dir)
            {
                try
                {
                    using var imageStream = imagePath.LoadImageFromPath(desiredWidth: 350);

                    var caption = await _inferenceService.GenerateImageCaption(imageStream, ct);
                    
                    ImageCollection.Add(new ImageItem(imagePath, imageStream)
                    {
                        MetaData = new MetaData
                        {
                            ImageClass = null,
                            ImageDescription = caption?.Caption,
                            AmbientColor = default
                        }
                    });
                }
                catch (FileNotFoundException ex)
                {
                    _dialogManager.Popup(NotificationType.Error, "File not found", ex.Message);
                }
            }
        }, ct);

    }
}