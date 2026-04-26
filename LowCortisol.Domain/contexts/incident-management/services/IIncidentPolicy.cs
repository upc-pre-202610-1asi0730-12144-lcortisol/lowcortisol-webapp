using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;

namespace LowCortisol.Domain.Contexts.IncidentManagement.Services;

public interface IIncidentPolicy
{
    bool CanBeAssigned(Incident incident);
    bool CanBeClosed(Incident incident);
}