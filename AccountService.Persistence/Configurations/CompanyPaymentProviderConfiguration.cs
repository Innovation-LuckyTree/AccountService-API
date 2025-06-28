using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class CompanyPaymentProviderConfiguration : IEntityTypeConfiguration<CompanyPaymentProvider>
{
    public void Configure(EntityTypeBuilder<CompanyPaymentProvider> builder)
    {
        builder.ToTable("CompanyPaymentProvider");
        builder.HasKey(e => e.CompanyPaymentProviderId);

        builder.Property(o => o.CompanyPaymentProviderId)
            .UseIdentityColumn(1, 1);

        builder.HasOne(o => o.PaymentProvider)
            .WithMany(f => f.CompanyPaymentProviders)
            .HasForeignKey(e => e.PaymentProviderId);
    }
}
