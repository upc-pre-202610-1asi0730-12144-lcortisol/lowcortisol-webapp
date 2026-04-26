using LowCortisol.Domain.Contexts.DeviceManagement.Aggregates;
using LowCortisol.Domain.Contexts.DeviceManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.OwnerUserId)
            .HasColumnName("owner_user_id")
            .IsRequired();

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(p => p.Value)
                .HasColumnName("name")
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Type, type =>
        {
            type.Property(p => p.Value)
                .HasColumnName("type")
                .HasMaxLength(60)
                .IsRequired();
        });

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

        builder.OwnsOne(x => x.Threshold, threshold =>
        {
            threshold.Property(p => p.Value)
                .HasColumnName("threshold_value");

            threshold.Property(p => p.Unit)
                .HasColumnName("threshold_unit")
                .HasMaxLength(20);
        });

        builder.Navigation(x => x.Sensors)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Sensors)
            .WithOne()
            .HasForeignKey("DeviceId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.DomainEvents);
    }
}