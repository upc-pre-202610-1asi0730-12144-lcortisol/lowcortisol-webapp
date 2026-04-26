using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LowCortisol.Infrastructure.Persistence.Configurations;

public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("user_accounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.OwnsOne(x => x.Name, name =>
        {
            name.Property(p => p.Value)
                .HasColumnName("name")
                .HasMaxLength(120)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(p => p.Value)
                .HasColumnName("email")
                .HasMaxLength(180)
                .IsRequired();

            email.HasIndex(p => p.Value)
                .IsUnique();
        });

        builder.OwnsOne(x => x.Phone, phone =>
        {
            phone.Property(p => p.Value)
                .HasColumnName("phone")
                .HasMaxLength(40);
        });

        builder.OwnsOne(x => x.PasswordHash, passwordHash =>
        {
            passwordHash.Property(p => p.Value)
                .HasColumnName("password_hash")
                .IsRequired();
        });

        builder.OwnsOne(x => x.Role, role =>
        {
            role.Property(p => p.Value)
                .HasColumnName("role")
                .HasMaxLength(40)
                .IsRequired();
        });

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}