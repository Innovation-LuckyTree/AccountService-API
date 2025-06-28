using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class DepositConfiguration : IEntityTypeConfiguration<Deposit>
{
    public void Configure(EntityTypeBuilder<Deposit> builder)
    {
        builder.ToTable("Deposit");
        builder.HasKey(e => e.DepositId);

        builder.Property(o => o.DepositId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.Id)
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

        builder.Property(o => o.ResponseAccountNumber)
            .IsRequired(false);

        builder.Property(o => o.ResponseAccountQR)
            .IsRequired(false);

        builder.Property(o => o.ResponseReferenceCode)
            .IsRequired(false);

        builder.HasOne(o => o.PaymentProvider)
            .WithMany(f => f.Deposits)
            .HasForeignKey(e => e.PaymentProviderId);
    }
}
