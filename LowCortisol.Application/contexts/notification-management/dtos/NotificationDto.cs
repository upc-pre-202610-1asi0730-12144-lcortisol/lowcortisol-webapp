namespace LowCortisol.Application.Contexts.NotificationManagement.DTOs;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid RecipientUserId { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAtUtc { get; set; }
    public int AttemptsCount { get; set; }
}