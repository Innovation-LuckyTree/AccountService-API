using AccountService.Application.Common.Constants;
using AccountService.Application.Requests.TransactionRequests.Commands.CreateTransaction;
using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.WithdrawAccount;

public class WithdrawAccountCommandHandler : IRequestHandler<WithdrawAccountCommand, WithdrawResponse>
{
    private readonly IPaymentApiService _paymentApiService;
    private readonly IMediator _mediator;
    private readonly string _baseCallback;

    public WithdrawAccountCommandHandler(IPaymentApiService paymentApiService, IMediator mediator, IAppConfig config)
    {
        _paymentApiService = paymentApiService;
        _mediator = mediator;
        _baseCallback = $"{config.PaymentApiClient.BaseAddressUrl}/api/transaction/callback";
    }

    public async Task<WithdrawResponse> Handle(WithdrawAccountCommand request, CancellationToken cancellationToken)
    {
        var transactionRequestId = await _mediator.Send(new CreateTransactionCommand(request.Amount, TransactionReferenceTypes.ACCOUNT_WITHDRAW, request.TransactionId), cancellationToken);
        
        var depositRequest = new WithdrawRequest
        {
            AccountId = request.AccountId,
            AccountName = request.AccountName,
            AccountNumber = request.AccountNumber,
            Amount = request.Amount,
            CallbackUrl = "",
            ClientTransactionId = request.TransactionId ?? "",
            ClientNotes = ""
        };

        if (transactionRequestId > 0)
        {
            depositRequest.ClientTransactionId = $"GC-TRN{transactionRequestId.ToString().PadLeft(10, '0')}";
            depositRequest.CallbackUrl = $"{_baseCallback}/{transactionRequestId}";
        }

        var result = await _paymentApiService.WithdrawAccount(depositRequest, cancellationToken);

        return result;
    }
}

