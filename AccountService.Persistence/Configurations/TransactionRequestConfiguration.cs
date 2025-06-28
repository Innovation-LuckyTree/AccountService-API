using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountService.Persistence.Configurations;

public class TransactionRequestConfiguration : IEntityTypeConfiguration<TransactionRequest>
{
    public void Configure(EntityTypeBuilder<TransactionRequest> builder)
    {
        builder.ToTable("TransactionRequest");
        builder.HasKey(e => e.TransactionRequestId);

        builder.Property(o => o.TransactionRequestId)
            .UseIdentityColumn(1, 1);

        builder.Property(o => o.TransactionType)
            .IsRequired(false);

        builder.Property(o => o.TransactionId)
            .IsRequired(false);
    }
}
