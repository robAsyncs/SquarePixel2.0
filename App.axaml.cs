using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SquarePixel.ViewModels;
using SquarePixel.Views;
using SquarePixel.Models;
using SquarePixel.Models.Entities;
using SquarePixel.Services;
using SukiUI.Dialogs;

namespace SquarePixel;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public static IServiceProvider Services { get; private set; } = default!;

    private IHost? _host;

    public override void OnFrameworkInitializationCompleted()
    {
        //BlobCache.ApplicationName = "SquarePixel";
      
        _host = new HostBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                
                services.AddDbContextFactory<SquareDbContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=randompassword"));
                
                //todo: fix connection string
                //postgresql://localhost:5432/mydb
                
                
                services.AddSingleton<ISukiDialogManager, SukiDialogManager>();
                services.AddSingleton<GalleryViewModel>();
                services.AddSingleton<LlmViewModel>();
                services.AddSingleton<ImageDbViewModel>();
                services.AddSingleton<SettingViewModel>();
                services.AddSingleton<RabbitMqService>();
                services.AddSingleton<DbService>();
                services.AddSingleton<SettingService<SquareSetting>, SettingService<SquareSetting>>();
            }).Build();

        Services = _host.Services;
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _host.Services.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }

}