using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;
using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;
using LowCortisol.Domain.Contexts.Monitoring.Aggregates;
using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;
using LowCortisol.Domain.Contexts.Reporting.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace LowCortisol.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<MonitoringSession> MonitoringSessions => Set<MonitoringSession>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<ConsumptionReport> ConsumptionReports => Set<ConsumptionReport>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}