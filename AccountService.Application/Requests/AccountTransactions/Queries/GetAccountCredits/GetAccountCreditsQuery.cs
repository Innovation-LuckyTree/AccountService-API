using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetAccountCredits;

public class GetAccountCreditsQuery : IRequest<AccountBalanceResponse>
{
}
