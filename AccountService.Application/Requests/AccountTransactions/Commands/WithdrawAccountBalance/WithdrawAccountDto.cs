using AccountService.Application.Common.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses;

namespace AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;

public class WithdrawAccountDto : BaseResponse<AccountBalanceResponse>
{
    public WithdrawAccountDto()
    {

    }

    public WithdrawAccountDto(AccountBalanceResponse AccountBalance)
    {
        Data = AccountBalance;
    }
}