using LowCortisol.Domain.Contexts.Reporting.Aggregates;
using LowCortisol.Domain.Contexts.Reporting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class ConsumptionReportConfiguration : IEntityTypeConfiguration<ConsumptionReport>
{
    public void Configure(EntityTypeBuilder<ConsumptionReport> builder)
    {
        builder.ToTable("consumption_reports");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.OwnerUserId)
            .HasColumnName("owner_user_id")
            .IsRequired();

        builder.OwnsOne(x => x.Period, period =>
        {
            period.Property(p => p.Value)
                .HasColumnName("period")
                .HasMaxLength(10)
                .IsRequired();
        });

        builder.OwnsOne(x => x.TotalWaterConsumption, water =>
        {
            water.Property(p => p.Value)
                .HasColumnName("total_water_consumption")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            water.Property(p => p.Unit)
                .HasColumnName("total_water_unit")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsOne(x => x.TotalGasConsumption, gas =>
        {
            gas.Property(p => p.Value)
                .HasColumnName("total_gas_consumption")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            gas.Property(p => p.Unit)
                .HasColumnName("total_gas_unit")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.Property(x => x.GeneratedAtUtc)
            .HasColumnName("generated_at_utc")
            .IsRequired();

        builder.Navigation(x => x.Lines)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("ConsumptionReportId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(x => x.DomainEvents);
    }
}