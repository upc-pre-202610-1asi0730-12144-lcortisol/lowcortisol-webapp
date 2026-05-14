using LowCortisol.Domain.Contexts.DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.ToTable("sensors");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property<Guid>("DeviceId")
            .HasColumnName("device_id")
            .IsRequired();

        builder.Property(x => x.SensorType)
            .HasColumnName("sensor_type")
            .HasMaxLength(60)
            .IsRequired();

        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(p => p.Value)
                .HasColumnName("status")
                .HasMaxLength(40)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Location, location =>
        {
            location.Property(p => p.Value)
                .HasColumnName("location")
                .HasMaxLength(120)
                .IsRequired();
        });
    }
}