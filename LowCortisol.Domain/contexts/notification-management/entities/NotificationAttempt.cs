using LowCortisol.Domain.Contexts.NotificationManagement.ValueObjects;

namespace LowCortisol.Domain.Contexts.NotificationManagement.Entities;

public class NotificationAttempt
{
    public Guid Id { get; private set; }
    public DateTime AttemptedAtUtc { get; private set; }
    public NotificationStatus Status { get; private set; }
    public string? FailureReason { get; private set; }

    private NotificationAttempt()
    {
        Id = Guid.Empty;
        Status = null!;
    }

    public NotificationAttempt(NotificationStatus status, string? failureReason = null)
    {
        Id = Guid.NewGuid();
        AttemptedAtUtc = DateTime.UtcNow;
        Status = status;
        FailureReason = string.IsNullOrWhiteSpace(failureReason) ? null : failureReason.Trim();
    }
}