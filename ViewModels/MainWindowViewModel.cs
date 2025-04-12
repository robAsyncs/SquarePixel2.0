using System;
using SukiUI.Dialogs;

namespace SquarePixel.ViewModels;

public partial class MainWindowViewModel(
    GalleryViewModel galleryViewModel,
    ISukiDialogManager dialogManager,
    SettingViewModel settingViewModel,
    LLMViewModel llmViewModel) : ViewModelBase
{
    public GalleryViewModel GalleryViewModel { get; } = galleryViewModel ?? throw new ArgumentNullException(nameof(galleryViewModel));
    public ISukiDialogManager DialogManager { get; } = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
    public SettingViewModel SettingViewModel { get; } = settingViewModel ?? throw new ArgumentNullException(nameof(settingViewModel));
    public LLMViewModel LlmViewModel { get; } = llmViewModel ?? throw new ArgumentNullException(nameof(llmViewModel));
}