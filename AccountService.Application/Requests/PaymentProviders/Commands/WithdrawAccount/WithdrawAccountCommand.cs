using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.WithdrawAccount;

public record WithdrawAccountCommand(string AccountId, decimal Amount, string AccountName, string AccountNumber, string? TransactionId) : IRequest<WithdrawResponse>;
