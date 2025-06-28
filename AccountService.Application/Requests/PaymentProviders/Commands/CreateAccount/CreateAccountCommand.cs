using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.CreateAccount;

public record CreateAccountCommand(CreateAccountRequest Account) : IRequest<AccountResponse>;
