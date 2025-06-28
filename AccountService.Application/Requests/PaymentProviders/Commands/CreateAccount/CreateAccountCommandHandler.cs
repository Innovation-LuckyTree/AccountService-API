using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
{
    private readonly IPaymentApiService _paymentApiService;

    public CreateAccountCommandHandler(IPaymentApiService paymentApiService)
    {
        _paymentApiService = paymentApiService;
    }

    public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        => await _paymentApiService.CreateAccount(request.Account, cancellationToken);
}