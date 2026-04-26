using LowCortisol.Domain.Contexts.NotificationManagement.Aggregates;
using LowCortisol.Domain.Contexts.NotificationManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.RecipientUserId)
            .HasColumnName("recipient_user_id")
            .IsRequired();

        builder.OwnsOne(x => x.Channel, channel =>
        {
            channel.Property(p => p.Value)
                .HasColumnName("channel")
                .HasMaxLength(30)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Message, message =>
        {
            message.Property(p => p.Title)
                .HasColumnName("title")
                .HasMaxLength(150)
                .IsRequired();

            message.Property(p => p.Body)
                .HasColumnName("body")
                .HasMaxLength(600)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Status, status =>
        {
            status.Property(p => p.Value)
                .HasColumnName("status")
                .HasMaxLength(30)
                .IsRequired();
        });

        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Navigation(x => x.Attempts)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Attempts)
            .WithOne()
            .HasForeignKey("NotificationId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.DomainEvents);
    }
}