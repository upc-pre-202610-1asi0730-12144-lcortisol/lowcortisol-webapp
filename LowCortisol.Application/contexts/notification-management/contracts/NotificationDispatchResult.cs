namespace LowCortisol.Application.Contexts.NotificationManagement.Contracts;

public class NotificationDispatchResult
{
    public bool Success { get; }
    public string? ErrorMessage { get; }

    private NotificationDispatchResult(bool success, string? errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    public static NotificationDispatchResult Ok()
    {
        return new NotificationDispatchResult(true, null);
    }

    public static NotificationDispatchResult Fail(string errorMessage)
    {
        return new NotificationDispatchResult(false, errorMessage);
    }
}