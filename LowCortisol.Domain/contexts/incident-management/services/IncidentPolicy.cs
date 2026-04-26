using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.IncidentManagement.Services;

public class IncidentPolicy : IIncidentPolicy
{
    public bool CanBeAssigned(Incident incident)
    {
        return incident.Status.Value == "Open" || incident.Status.Value == "Assigned";
    }

    public bool CanBeClosed(Incident incident)
    {
        return incident.Status.Value == "Resolved";
    }
}