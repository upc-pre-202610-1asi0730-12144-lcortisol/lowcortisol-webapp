namespace LowCortisol.Domain.Contexts.IncidentManagement.Entities;

public class IncidentHistoryEntry
{
    public Guid Id { get; private set; }
    public string Action { get; private set; }
    public string Description { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }

    private IncidentHistoryEntry()
    {
        Id = Guid.Empty;
        Action = string.Empty;
        Description = string.Empty;
    }

    public IncidentHistoryEntry(string action, string description)
    {
        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action cannot be empty.", nameof(action));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        Id = Guid.NewGuid();
        Action = action.Trim();
        Description = description.Trim();
        OccurredOnUtc = DateTime.UtcNow;
    }
}