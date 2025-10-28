using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class ImageDbView : ReactiveUserControl<ImageDbViewModel>
{
    private static Window GetMainWindow()
    {
        if (Application.Current!.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            return desktop.MainWindow!;
        return null!;
    }
    
    public ImageDbView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            DataContext = Locator.Current.GetService<ImageDbViewModel>();

            ViewModel!.PickFileInteraction.
                RegisterHandler(async interaction =>
                {
                    var files = await GetMainWindow()
                        .StorageProvider.OpenFilePickerAsync(
                            new FilePickerOpenOptions
                            {
                                Title = "Select an Image",
                                AllowMultiple = false,
                                FileTypeFilter = new[]
                                {
                                    new FilePickerFileType("Image Files")
                                    {
                                        Patterns = new[] { "*.png", "*.jpg", "*.jpeg" }
                                    }
                                }
                            });
                
                    interaction.SetOutput(files);
                });
        });
    }
    
   
}