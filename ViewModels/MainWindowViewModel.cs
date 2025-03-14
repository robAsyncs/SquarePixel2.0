using System;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class MainWindowViewModel(GalleryViewModel galleryViewModel, ISukiDialogManager dialogManager) : ViewModelBase
{
    public GalleryViewModel GalleryViewModel { get; } = galleryViewModel ?? throw new ArgumentNullException(nameof(galleryViewModel));
    public ISukiDialogManager DialogManager { get; } = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
}