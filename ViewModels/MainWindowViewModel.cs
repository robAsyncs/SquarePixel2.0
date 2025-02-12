using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public GalleryViewModel GalleryViewModel { get; }
    public ISukiDialogManager DialogManager { get; } = new SukiDialogManager();

    public MainWindowViewModel()
    {
        GalleryViewModel = new GalleryViewModel(DialogManager);
    }
}