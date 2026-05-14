using LowCortisol.Application.Contexts.NotificationManagement.Contracts;
using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;

namespace LowCortisol.Infrastructure.Notifications;

public class ConsoleNotificationSender : INotificationSender
{
    public Task<NotificationDispatchResult> SendAsync(
        Notification notification,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Console.WriteLine("=== LOWCORTISOL NOTIFICATION ===");
            Console.WriteLine($"NotificationId: {notification.Id}");
            Console.WriteLine($"RecipientUserId: {notification.RecipientUserId}");
            Console.WriteLine($"Channel: {notification.Channel.Value}");
            Console.WriteLine($"Title: {notification.Message.Title}");
            Console.WriteLine($"Body: {notification.Message.Body}");
            Console.WriteLine($"CreatedAtUtc: {notification.CreatedAtUtc:O}");
            Console.WriteLine("================================");

            return Task.FromResult(NotificationDispatchResult.Ok());
        }
        catch (Exception ex)
        {
            return Task.FromResult(NotificationDispatchResult.Fail(ex.Message));
        }
    }
}