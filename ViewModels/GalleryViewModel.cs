using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class GalleryViewModel
    : ViewModelBase
{
    public ObservableCollection<ImageItem> ImageCollection { get; } = [];
    private ISukiDialogManager _dialogManager;
  
    private IObservable<bool> _canSelectFile = Observable.Return(true);
    [Reactive] private string? _dirName;

    public GalleryViewModel( ISukiDialogManager dialogManager)
    {
        _dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
        
    }
    
    
    [ReactiveCommand(CanExecute = nameof(_canSelectFile))]
    private async Task LoadGalleryFolder(string? dirName)
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
            //todo: make dialog manager
            return;
        }
        
        foreach (var fileInfo in dir)
          ImageCollection.Add(new ImageItem(fileInfo));
    }


    
    
    private async Task LoadImageThumbnailsAsync()
    {
        
    }
}