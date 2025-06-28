using AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalanceByAccount;

public class WithdrawAccountBalanceByAccountCommand : IRequest<WithdrawAccountDto>
{
    public Guid AccountId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public string ModeOfTransaction { get; set; }
    
}
