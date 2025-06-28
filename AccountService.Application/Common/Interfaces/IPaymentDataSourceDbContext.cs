using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Common.Interfaces;

public interface IPaymentDataSourceDbContext
{
    DbSet<CompanyPaymentProvider> CompanyPaymentProviders { get; set; }
    DbSet<Deposit> Deposits { get; set; }
    DbSet<DepositToken> DepositTokens { get; set; }
    DbSet<PaymentProvider> PaymentProviders { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Withdrawal> Withdrawals { get; set; }
    DbSet<TransactionRequest> TransactionRequests { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}