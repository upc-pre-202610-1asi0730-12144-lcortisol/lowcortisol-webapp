using LowCortisol.Application.Contexts.NotificationManagement.DTOs;
using LowCortisol.Application.Contexts.NotificationManagement.Queries;
using LowCortisol.Application.Contexts.NotificationManagement.Services;

namespace LowCortisol.Application.Contexts.NotificationManagement.Handlers;

public class GetNotificationsByRecipientHandler
{
    private readonly INotificationManagementService _notificationManagementService;

    public GetNotificationsByRecipientHandler(INotificationManagementService notificationManagementService)
    {
        _notificationManagementService = notificationManagementService;
    }

    public Task<IReadOnlyCollection<NotificationDto>> HandleAsync(
        GetNotificationsByRecipientQuery query,
        CancellationToken cancellationToken = default)
    {
        return _notificationManagementService.GetByRecipientAsync(query.RecipientUserId, cancellationToken);
    }
}