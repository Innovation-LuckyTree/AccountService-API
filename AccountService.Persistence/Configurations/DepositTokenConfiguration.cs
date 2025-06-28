using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class DepositTokenConfiguration : IEntityTypeConfiguration<DepositToken>
{
    public void Configure(EntityTypeBuilder<DepositToken> builder)
    {
        builder.ToTable("DepositToken");
        builder.HasKey(e => e.DepositTokenId);

        builder.Property(o => o.DepositTokenId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.Base)
            .IsRequired(false);

        builder.Property(o => o.Token)
            .IsRequired(false);

        builder.Property(o => o.Url)
            .IsRequired(false);

        builder.Property(o => o.Status)
            .IsRequired(false);

        builder.Property(o => o.AccountName)
            .IsRequired(false);

        builder.Property(o => o.AccountNumber)
            .IsRequired(false);

        builder.Property(o => o.ClientTransactionId)
            .IsRequired(false);

        builder.Property(o => o.ClientNotes)
            .IsRequired(false);

        builder.Property(o => o.CallbackUrl)
            .IsRequired(false);

        builder.Property(o => o.RedirectUrl)
            .IsRequired(false);

        builder.HasOne(o => o.PaymentProvider)
            .WithMany(f => f.DepositTokens)
            .HasForeignKey(e => e.PaymentProviderId);
    }
}
