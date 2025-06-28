using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.RbgiWithdrawAccountBalance;

public record RbgiWithdrawAccountBalanceCommand(Guid AccountObjectId, string AccountId, decimal Amount, string AccountName, string AccountNumber, string TransactionId) : IRequest<RbgiWithdrawData>;
