using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models.AI;

namespace SquarePixel.ViewModels;

public partial class LlmViewModel : ViewModelBase
{
    private static readonly Conversation IntroConversation = 
        new(MessageSource.Model, "Hi I'm square GPT, How can I help Today?");

    [Reactive] private string? _userPrompt;
    public ObservableCollection<Conversation> ConversationHistory { get; } = [IntroConversation];
    
    
    //Use natural language to query for images
    [ReactiveCommand] private async Task AskModelAsync()
    {
        
        
        if (string.IsNullOrWhiteSpace(UserPrompt)) return;
        ConversationHistory.Add(new Conversation(MessageSource.User, UserPrompt));
        
        //inject model context if necessary
        //await inferenceService.ChatBotConversationAsync(UserPrompt, string.Empty);
        UserPrompt = string.Empty;
    }

    [ReactiveCommand]
    private void ClearConversation()
    {
        ConversationHistory.Clear();
        ConversationHistory.Add(IntroConversation);
        
    }
}