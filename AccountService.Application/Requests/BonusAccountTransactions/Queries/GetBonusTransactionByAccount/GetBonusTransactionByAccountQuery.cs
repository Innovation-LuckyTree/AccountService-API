using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByAccount;

public record GetBonusTransactionByAccountQuery(Guid AccountId) : IRequest<BonusAccountDto>;

