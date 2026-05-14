using LowCortisol.Domain.Contexts.IncidentManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class IncidentHistoryEntryConfiguration : IEntityTypeConfiguration<IncidentHistoryEntry>
{
    public void Configure(EntityTypeBuilder<IncidentHistoryEntry> builder)
    {
        builder.ToTable("incident_history_entries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property<Guid>("IncidentId")
            .HasColumnName("incident_id")
            .IsRequired();

        builder.Property(x => x.Action)
            .HasColumnName("action")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.OccurredOnUtc)
            .HasColumnName("occurred_on_utc")
            .IsRequired();
    }
}