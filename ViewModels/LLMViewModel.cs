using System.Collections.ObjectModel;
using ReactiveUI.SourceGenerators;
using SquarePixel.Models.AI;

namespace SquarePixel.ViewModels;

public partial class LLMViewModel: ViewModelBase
{
    public LLMViewModel()
    {
        
    }

    [Reactive] private string? _userPrompt;
    public ObservableCollection<Conversation> ConversationHistory { get; } = [
    new Conversation(MessageSource.Model, "How can I help today?"), new Conversation(MessageSource.User, "What time is it?")
    ];
}