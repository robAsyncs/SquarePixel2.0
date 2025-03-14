using Avalonia.Controls.Notifications;
using SukiUI.Dialogs;

namespace SquarePixel.Extensions;

public static class DialogEx
{
    public static void Popup(
        this ISukiDialogManager dialogManager, 
        NotificationType type,
        string title, 
        string Message)
    {
        dialogManager.CreateDialog()
            .OfType(type)
            .WithTitle(title)
            .WithContent(Message)
            .Dismiss()
            .ByClickingBackground()
            .TryShow();
    }
    
    
}