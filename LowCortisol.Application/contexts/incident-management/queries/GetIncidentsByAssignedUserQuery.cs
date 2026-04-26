namespace LowCortisol.Application.Contexts.IncidentManagement.Queries;

public class GetIncidentsByAssignedUserQuery
{
    public Guid AssignedUserId { get; set; }
}