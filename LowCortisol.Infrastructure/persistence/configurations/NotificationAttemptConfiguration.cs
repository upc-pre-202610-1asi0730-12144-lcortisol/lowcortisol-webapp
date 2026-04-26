using LowCortisol.Domain.Contexts.NotificationManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class NotificationAttemptConfiguration : IEntityTypeConfiguration<NotificationAttempt>
{
    public void Configure(EntityTypeBuilder<NotificationAttempt> builder)
    {
        builder.ToTable("notification_attempts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property<Guid>("NotificationId")
            .HasColumnName("notification_id")
            .IsRequired();

        builder.Property(x => x.AttemptedAtUtc)
            .HasColumnName("attempted_at_utc")
            .IsRequired();

        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(p => p.Value)
                .HasColumnName("status")
                .HasMaxLength(30)
                .IsRequired();
        });

        builder.Property(x => x.FailureReason)
            .HasColumnName("failure_reason")
            .HasMaxLength(300);
    }
}