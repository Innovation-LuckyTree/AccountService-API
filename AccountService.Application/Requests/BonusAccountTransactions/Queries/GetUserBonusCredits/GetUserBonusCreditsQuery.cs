using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetUserBonusCredits;

public record GetUserBonusCreditsQuery(Guid AccountId) : IRequest<BonusAccountBalanceResponse>;
