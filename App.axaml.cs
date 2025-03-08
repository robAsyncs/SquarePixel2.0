using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SquarePixel.ViewModels;
using SquarePixel.Views;
using Splat;
using SukiUI.Dialogs;

namespace SquarePixel;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
         InitializeDependencies();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
           
            desktop.MainWindow = new MainWindow
            {
                DataContext = Locator.Current.GetService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }


    public void InitializeDependencies()
    
    {  
        SplatRegistrations.RegisterLazySingleton<ISukiDialogManager, SukiDialogManager>();
        SplatRegistrations.RegisterLazySingleton<GalleryViewModel, GalleryViewModel>();
        SplatRegistrations.RegisterLazySingleton<MainWindowViewModel, MainWindowViewModel>();
        
      
    }

}