using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;

public class AddWalletToAccountCommand : IRequest<AccountBalanceResponse>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public string TransactionReference { get; set; }
    public decimal Amount { get; set; }
    public string ModeOfTransaction { get; set; }
    public string Notes { get; set; }
    public string? AccountType { get; set; }
}
