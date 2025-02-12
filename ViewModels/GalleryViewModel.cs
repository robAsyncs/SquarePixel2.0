using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SukiUI.Controls;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class GalleryViewModel(
    ISukiDialogManager dialogManager) 
    : ViewModelBase
{
    public ObservableCollection<ImageItem> ImageCollection { get; } = [];
    private ISukiDialogManager _dialogManager { get; } = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));


    [ReactiveCommand]
    private async Task LoadGalleryFolderAsync()
    {
        var path = string.Empty;
        _dialogManager.CreateDialog()
            .Dismiss().ByClickingBackground()
            .WithTitle("Select Image or Folder").TryShow();
        
        
        //todo: check if folder, load all item else load image add to ObsColl
    }
}