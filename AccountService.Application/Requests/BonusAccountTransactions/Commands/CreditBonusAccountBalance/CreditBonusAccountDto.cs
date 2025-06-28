using AccountService.Application.Common.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

namespace AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;

public class CreditBonusAccountDto : BaseResponse<BonusAccountBalanceResponse>
{
    public CreditBonusAccountDto(BonusAccountBalanceResponse BonusAccountBalance)
    {
        Data = BonusAccountBalance;
    }
}
