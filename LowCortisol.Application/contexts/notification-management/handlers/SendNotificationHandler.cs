using LowCortisol.Application.Contexts.NotificationManagement.Commands;
using LowCortisol.Application.Contexts.NotificationManagement.Services;

namespace LowCortisol.Application.Contexts.NotificationManagement.Handlers;

public class SendNotificationHandler
{
    private readonly INotificationManagementService _notificationManagementService;

    public SendNotificationHandler(INotificationManagementService notificationManagementService)
    {
        _notificationManagementService = notificationManagementService;
    }

    public Task<(bool Success, string Error)> HandleAsync(
        SendNotificationCommand command,
        CancellationToken cancellationToken = default)
    {
        return _notificationManagementService.SendAsync(command, cancellationToken);
    }
}