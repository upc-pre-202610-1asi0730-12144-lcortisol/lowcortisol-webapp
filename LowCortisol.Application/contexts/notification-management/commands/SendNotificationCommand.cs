namespace LowCortisol.Application.Contexts.NotificationManagement.Commands;

public class SendNotificationCommand
{
    public Guid RecipientUserId { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}