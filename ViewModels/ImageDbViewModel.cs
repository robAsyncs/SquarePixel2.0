using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Controls.Notifications;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Extensions;
using SquarePixel.Models;
using SquarePixel.Util;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class ImageDbViewModel: ViewModelBase
{
    //replace with dynamic data for pagination and filtering
    public ObservableCollection<ImageItem> ImageCollection { get; } = [];
    private ISukiDialogManager _dialogManager { get; }
    
    //todo: replace with dynamic data coll
    public ObservableCollection<string> ImageClasses { get; } = ["Planes", "Buildings"];
    [Reactive] public int _filterByClassValue;

    public ImageDbViewModel(ISukiDialogManager dialogManager)
    {
        _dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
        
        this.WhenActivated(disposable =>
        {
            this.WhenAnyValue(x => x.FilterByClassValue)
                .InvokeCommand(FilterBySelectedClassCommand).DisposeWith(disposable);
            
            LoadGalleryFolderCommand.Execute(@"C:\\Users\\robel\\Desktop\\OneDrive\\Gallery\\Shared Gallery Folder\\Mk Share")
                .Subscribe().DisposeWith(disposable);
        });
    }


    


    [ReactiveCommand]
    private async Task FilterBySelectedClass(int classPosition)
    {
        
    }
    
    
    
    [ReactiveCommand] private async Task LoadGalleryFolder(string? dirName)
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
        await Task.Run(() =>
        {
            foreach (var fileInfo in dir)
                ImageCollection.Add(new ImageItem(fileInfo.LoadImageFromPath(width:350)));
        });

    }
}