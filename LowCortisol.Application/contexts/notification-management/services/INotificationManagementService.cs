using LowCortisol.Application.Contexts.NotificationManagement.Commands;
using LowCortisol.Application.Contexts.NotificationManagement.DTOs;

namespace LowCortisol.Application.Contexts.NotificationManagement.Services;

public interface INotificationManagementService
{
    Task<(bool Success, string Error)> SendAsync(
        SendNotificationCommand command,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<NotificationDto>> GetByRecipientAsync(
        Guid recipientUserId,
        CancellationToken cancellationToken = default);
}