using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models;
using SquarePixel.Services;

namespace SquarePixel.ViewModels;

public partial class SettingViewModel: ViewModelBase
{
    [Reactive] private string _imageDir;
    [Reactive] private string _inferenceServerAddress;
    private SettingService<SquareSetting> _settingService;


    public SettingViewModel(SettingService<SquareSetting> settingService)
    {
        _settingService = settingService;
        
        this.WhenActivated(disposable =>
        {
            LoadSettingsCommand.Execute().Subscribe().DisposeWith(disposable);
        });
    }
    
    [ReactiveCommand] private async Task LoadSettingsAsync(CancellationToken ct)
    {
        var setting = await _settingService.GetAsync<SquareSetting>(ct);
        ImageDir = setting.ImageDataBasePath;
        InferenceServerAddress = setting.InferenceServer;
        
        
    }


    [ReactiveCommand] private async Task SaveSettingsAsync(CancellationToken ct)
    {
        var newSetting = new SquareSetting
        {
            ImageDataBasePath = ImageDir,
            InferenceServer = InferenceServerAddress,
            MaxImageWidth = 350
        };

        await _settingService.SaveSettingAsync(newSetting);
    }
}