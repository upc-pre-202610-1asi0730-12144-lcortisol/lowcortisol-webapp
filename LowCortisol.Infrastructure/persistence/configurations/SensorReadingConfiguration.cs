using LowCortisol.Domain.Contexts.Monitoring.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
{
    public void Configure(EntityTypeBuilder<SensorReading> builder)
    {
        builder.ToTable("sensor_readings");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property<Guid>("MonitoringSessionId")
            .HasColumnName("monitoring_session_id")
            .IsRequired();

        builder.Property(x => x.DeviceId)
            .HasColumnName("device_id")
            .IsRequired();

        builder.Property(x => x.SensorId)
            .HasColumnName("sensor_id")
            .IsRequired();

        builder.OwnsOne(x => x.SensorType, sensorType =>
        {
            sensorType.Property(p => p.Value)
                .HasColumnName("sensor_type")
                .HasMaxLength(40)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Value, readingValue =>
        {
            readingValue.Property(p => p.Value)
                .HasColumnName("reading_value")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Unit, unit =>
        {
            unit.Property(p => p.Value)
                .HasColumnName("unit")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.Property(x => x.RecordedAtUtc)
            .HasColumnName("recorded_at_utc")
            .IsRequired();
    }
}