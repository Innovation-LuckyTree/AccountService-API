using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusAccountCredits;

public class GetBonusAccountCreditsQuery : IRequest<BonusAccountBalanceResponse>
{
}
