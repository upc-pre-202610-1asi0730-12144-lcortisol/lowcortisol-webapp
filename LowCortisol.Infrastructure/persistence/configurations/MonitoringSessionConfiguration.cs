using LowCortisol.Domain.Contexts.Monitoring.Aggregates;
using LowCortisol.Domain.Contexts.Monitoring.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class MonitoringSessionConfiguration : IEntityTypeConfiguration<MonitoringSession>
{
    public void Configure(EntityTypeBuilder<MonitoringSession> builder)
    {
        builder.ToTable("monitoring_sessions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.DeviceId)
            .HasColumnName("device_id")
            .IsRequired();

        builder.Property(x => x.StartedAtUtc)
            .HasColumnName("started_at_utc")
            .IsRequired();

        builder.Navigation(x => x.Readings)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.Events)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Readings)
            .WithOne()
            .HasForeignKey("MonitoringSessionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Events)
            .WithOne()
            .HasForeignKey("MonitoringSessionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.DomainEvents);
    }
}