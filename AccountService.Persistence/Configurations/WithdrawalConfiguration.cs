using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class WithdrawalConfiguration : IEntityTypeConfiguration<Withdrawal>
{
    public void Configure(EntityTypeBuilder<Withdrawal> builder)
    {
        builder.ToTable("Withdrawal");
        builder.HasKey(e => e.WithdrawalId);

        builder.Property(o => o.WithdrawalId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.ResponseId)
            .IsRequired(false);

        builder.Property(o => o.AccountId)
            .IsRequired(false);

        builder.Property(o => o.Type)
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
            .WithMany(f => f.Withdrawals)
            .HasForeignKey(e => e.PaymentProviderId);
    }
}
