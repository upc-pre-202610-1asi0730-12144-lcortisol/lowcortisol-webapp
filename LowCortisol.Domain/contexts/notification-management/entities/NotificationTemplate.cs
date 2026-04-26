namespace LowCortisol.Domain.Contexts.NotificationManagement.Entities;

public class NotificationTemplate
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string SubjectPattern { get; private set; }
    public string BodyPattern { get; private set; }

    private NotificationTemplate()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        SubjectPattern = string.Empty;
        BodyPattern = string.Empty;
    }

    public NotificationTemplate(string name, string subjectPattern, string bodyPattern)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Template name cannot be empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(subjectPattern))
            throw new ArgumentException("Template subject pattern cannot be empty.", nameof(subjectPattern));

        if (string.IsNullOrWhiteSpace(bodyPattern))
            throw new ArgumentException("Template body pattern cannot be empty.", nameof(bodyPattern));

        Id = Guid.NewGuid();
        Name = name.Trim();
        SubjectPattern = subjectPattern.Trim();
        BodyPattern = bodyPattern.Trim();
    }
}