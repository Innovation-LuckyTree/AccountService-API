using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class PaymentProviderConfiguration : IEntityTypeConfiguration<PaymentProvider>
{
    public void Configure(EntityTypeBuilder<PaymentProvider> builder)
    {
        builder.ToTable("PaymentProvider");
        builder.HasKey(e => e.PaymentProviderId);

        builder.Property(o => o.PaymentProviderId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(o => o.Description)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(o => o.Configuration)
            .IsRequired(false);
    }
}
