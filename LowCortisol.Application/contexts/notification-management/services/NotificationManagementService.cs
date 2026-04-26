using LowCortisol.Application.Common.Interfaces;
using LowCortisol.Application.Contexts.NotificationManagement.Commands;
using LowCortisol.Application.Contexts.NotificationManagement.Contracts;
using LowCortisol.Application.Contexts.NotificationManagement.DTOs;
using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;
using LowCortisol.Domain.Contexts.NotificationManagement.Repositories;
using LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

namespace LowCortisol.Application.Contexts.NotificationManagement.Services;

public class NotificationManagementService : INotificationManagementService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationSender _notificationSender;
    private readonly IUnitOfWork _unitOfWork;
    
    public NotificationManagementService(
        INotificationRepository notificationRepository,
        INotificationSender notificationSender,
        IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _notificationSender = notificationSender;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> SendAsync(
        SendNotificationCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (command.RecipientUserId == Guid.Empty)
                return (false, "Recipient user id is required.");

            var notification = new Notification(
                command.RecipientUserId,
                new NotificationChannel(command.Channel),
                new NotificationMessage(command.Title, command.Body)
            );

            await _notificationRepository.AddAsync(notification, cancellationToken);

            var dispatchResult = await _notificationSender.SendAsync(notification, cancellationToken);

            if (dispatchResult.Success)
                notification.MarkAsSent();
            else
                notification.MarkAsFailed(dispatchResult.ErrorMessage ?? "Unknown notification error.");

            await _notificationRepository.UpdateAsync(notification, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return dispatchResult.Success
                ? (true, string.Empty)
                : (false, dispatchResult.ErrorMessage ?? "Notification dispatch failed.");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<NotificationDto>> GetByRecipientAsync(
        Guid recipientUserId,
        CancellationToken cancellationToken = default)
    {
        var notifications = await _notificationRepository.FindByRecipientUserIdAsync(recipientUserId, cancellationToken);

        return notifications.Select(MapToDto).ToList();
    }

    private static NotificationDto MapToDto(Notification notification)
    {
        return new NotificationDto
        {
            Id = notification.Id,
            RecipientUserId = notification.RecipientUserId,
            Channel = notification.Channel.Value,
            Title = notification.Message.Title,
            Body = notification.Message.Body,
            Status = notification.Status.Value,
            CreatedAtUtc = notification.CreatedAtUtc,
            AttemptsCount = notification.Attempts.Count
        };
    }
}