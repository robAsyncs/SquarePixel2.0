using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models.AI;
using SquarePixel.Services;

namespace SquarePixel.ViewModels;

public partial class LlmViewModel(InferenceService inferenceService) : ViewModelBase
{
    private static readonly Conversation IntroConversation = 
        new(MessageSource.Model, "Hi I'm square GPT, How can I help Today?");

    [Reactive] private string? _userPrompt;
    public ObservableCollection<Conversation> ConversationHistory { get; } = [IntroConversation];

    [ReactiveCommand] private async Task AskModelAsync()
    {
        if (string.IsNullOrWhiteSpace(UserPrompt)) return;
        ConversationHistory.Add(new Conversation(MessageSource.User, UserPrompt));
        UserPrompt = string.Empty;

        
        await inferenceService.ChatBotConversationAsync();
    }

    [ReactiveCommand]
    private void ClearConversation()
    {
        ConversationHistory.Clear();
        ConversationHistory.Add(IntroConversation);
        
    }
}