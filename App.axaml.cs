using Akavache;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using SquarePixel.ViewModels;
using SquarePixel.Views;
using Splat;
using SquarePixel.Interface;
using SquarePixel.Models;
using SquarePixel.Services;
using SquarePixel.Views.Contorls;
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
        BlobCache.ApplicationName = "SquarePixel";
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


    private void InitializeDependencies()
    
    {   
      
        SplatRegistrations.RegisterLazySingleton<ISukiDialogManager, SukiDialogManager>();
        SplatRegistrations.RegisterLazySingleton<GalleryViewModel, GalleryViewModel>();
        SplatRegistrations.RegisterLazySingleton<ImageDbViewModel, ImageDbViewModel>();
        SplatRegistrations.RegisterLazySingleton<MainWindowViewModel, MainWindowViewModel>();
        SplatRegistrations.RegisterLazySingleton<SettingViewModel, SettingViewModel>();
        SplatRegistrations.RegisterLazySingleton<InferenceService, InferenceService>();
        SplatRegistrations.RegisterLazySingleton<LLMViewModel, LLMViewModel>();
        SplatRegistrations.RegisterLazySingleton<SettingService<Setting>, SettingService<Setting>>();


    }

}