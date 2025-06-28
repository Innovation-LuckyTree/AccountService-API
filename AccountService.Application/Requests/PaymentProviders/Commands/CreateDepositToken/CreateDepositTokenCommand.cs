using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.CreateDepositToken;

public record CreateDepositTokenCommand(string MerchantName, string AccountId, decimal Amount, string AccountName, string? TransactionType, string? TransactionId) : IRequest<DepositTokenResponse>;
