using LowCortisol.Domain.Contexts.Reporting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class ReportLineConfiguration : IEntityTypeConfiguration<ReportLine>
{
    public void Configure(EntityTypeBuilder<ReportLine> builder)
    {
        builder.ToTable("report_lines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property<Guid>("ConsumptionReportId")
            .HasColumnName("consumption_report_id")
            .IsRequired();

        builder.Property(x => x.Label)
            .HasColumnName("label")
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(x => x.WaterConsumption, water =>
        {
            water.Property(p => p.Value)
                .HasColumnName("water_consumption")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            water.Property(p => p.Unit)
                .HasColumnName("water_unit")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsOne(x => x.GasConsumption, gas =>
        {
            gas.Property(p => p.Value)
                .HasColumnName("gas_consumption")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            gas.Property(p => p.Unit)
                .HasColumnName("gas_unit")
                .HasMaxLength(20)
                .IsRequired();
        });
    }
}