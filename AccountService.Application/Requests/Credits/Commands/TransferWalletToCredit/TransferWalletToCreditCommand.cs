using AccountService.Application.Common.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Commands.TransferWalletToCredit;

public class TransferWalletToCreditCommand : IRequest<BaseResponse<AccountBalanceResponse>>
{
    public Guid AccountWalletId { get; set; }
    public Guid AccountCreditId { get; set; }
    public string TransactionNo { get; set; }
    public decimal Amount { get; set; }
    public string Notes { get; set; }
    public string ModeOfTransaction { get; set; }
}
