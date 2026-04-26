using LowCortisol.Domain.Contexts.IncidentManagement.Aggregates;
using LowCortisol.Domain.Contexts.IncidentManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.ToTable("incidents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.DeviceId)
            .HasColumnName("device_id")
            .IsRequired();

        builder.Property(x => x.ReportedByUserId)
            .HasColumnName("reported_by_user_id")
            .IsRequired();

        builder.Property(x => x.AssignedUserId)
            .HasColumnName("assigned_user_id");

        builder.OwnsOne(x => x.Severity, severity =>
        {
            severity.Property(p => p.Value)
                .HasColumnName("severity")
                .HasMaxLength(30)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(p => p.Value)
                .HasColumnName("status")
                .HasMaxLength(30)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Description, description =>
        {
            description.Property(p => p.Value)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Navigation(x => x.History)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.History)
            .WithOne()
            .HasForeignKey("IncidentId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.DomainEvents);
    }
}