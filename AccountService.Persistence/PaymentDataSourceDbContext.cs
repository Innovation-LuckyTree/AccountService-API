using AccountService.Application.Common.Interfaces;
using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Persistence;

public class PaymentDataSourceDbContext : DbContext, IPaymentDataSourceDbContext
{
    public PaymentDataSourceDbContext()
    {
    }

    public PaymentDataSourceDbContext(DbContextOptions<PaymentDataSourceDbContext> options)
        : base(options)
    {
    }

    public DbSet<CompanyPaymentProvider> CompanyPaymentProviders { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<DepositToken> DepositTokens { get; set; }
    public DbSet<PaymentProvider> PaymentProviders { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }
    public DbSet<TransactionRequest> TransactionRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "1.1.1-servicing");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentDataSourceDbContext).Assembly);
    }
}
