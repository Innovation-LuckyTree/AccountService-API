using AccountService.Infrastructure.WalletApi.Models.Responses;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetCurrentTotalTransaction;

public class AccountTransactionDto
{
    public AccountBalanceResponse WalletBalance { get; set; }
    public AccountBalanceResponse CreditBalance { get; set; }
    public decimal TotalCashIn { get; set; }
    public decimal TotalCashOut { get; set; }
    public decimal TotalBetAmount { get; set; }
    public int TotalCashInCount { get; set; }
    public int TotalCashOutCount { get; set; }
    public DateTime Date { get; set; }
}