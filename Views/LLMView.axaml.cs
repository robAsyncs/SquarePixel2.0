using System;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using Splat;
using SquarePixel.ViewModels;

namespace SquarePixel.Views;

public partial class LlmView : ReactiveUserControl<LlmViewModel>
{
    public LlmView()
    {
        InitializeComponent();
      
        DataContext = Locator.Current.GetService<LlmViewModel>() ?? throw new ArgumentNullException();
    }

    private void PromptModel(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter || DataContext is not LlmViewModel vm) return;
        
        vm.AskModelCommand.Execute().Subscribe();
        e.Handled = true;
    }
}