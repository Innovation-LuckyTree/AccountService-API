using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Queries.GetUserAccountCredits;

public record GetUserAccountCreditsQuery(Guid AccountId) : IRequest<AccountBalanceResponse>;
