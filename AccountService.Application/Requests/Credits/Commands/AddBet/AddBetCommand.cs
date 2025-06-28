using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Commands.AddBet;

public class AddBetCommand : IRequest<AccountBalanceResponse>
{
    public Guid AccountCreditId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
}
