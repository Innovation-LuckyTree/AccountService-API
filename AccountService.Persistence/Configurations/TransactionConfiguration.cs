using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");
        builder.HasKey(e => e.TransactionId);

        builder.Property(o => o.TransactionId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.TransactionObjectId);

        builder.Property(o => o.ResponseId)
            .IsRequired(false);

        builder.Property(o => o.AccountId)
            .IsRequired(false);

        builder.Property(o => o.Type)
            .IsRequired(false);

        builder.Property(o => o.Status)
            .IsRequired(false);

        builder.Property(o => o.StatusNotes)
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

        builder.Property(o => o.TransactionRequestId)
            .IsRequired(false);

        builder.HasOne(o => o.PaymentProvider)
            .WithMany(f => f.Transactions)
            .HasForeignKey(e => e.PaymentProviderId);
    }
}
