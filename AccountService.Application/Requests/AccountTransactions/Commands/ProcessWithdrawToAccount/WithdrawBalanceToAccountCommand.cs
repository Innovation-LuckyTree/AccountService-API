using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.ProcessWithdrawToAccount;

public class ProcessWithdrawToAccountCommand : IRequest<AccountBalanceResponse>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public string ModeOfTransaction { get; set; }
    public bool Success { get; set; }
}
