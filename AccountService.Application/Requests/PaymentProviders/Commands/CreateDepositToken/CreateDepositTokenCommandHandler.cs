using System.Text.Json;
using AccountService.Application.Requests.TransactionRequests.Commands.CreateTransaction;
using AccountService.Common.Interfaces;
using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.CreateDepositToken;

public class CreateDepositTokenCommandHandler : IRequestHandler<CreateDepositTokenCommand, DepositTokenResponse>
{
    private readonly IPaymentApiService _paymentApiService;
    private readonly IMediator _mediator;
    private readonly string _baseCallback;
    private readonly ICurrentUserService _currentUserService;

    public CreateDepositTokenCommandHandler(IPaymentApiService paymentApiService, IAppConfig config, IMediator mediator, ICurrentUserService currentUserService)
    {
        _paymentApiService = paymentApiService;
        _mediator = mediator;
        _baseCallback = $"{config.PaymentApiClient.BaseAddressUrl}/api/transaction/callback";
        _currentUserService = currentUserService;
    }

    public async Task<DepositTokenResponse> Handle(CreateDepositTokenCommand request, CancellationToken cancellationToken)
    {
        var transactionRequestId = await _mediator.Send(new CreateTransactionCommand(request.Amount, request.TransactionType, request.TransactionId), cancellationToken);

        var depositRequest = new DepositTokenRequest
        {
            MerchantName = request.MerchantName,
            AccountId = request.AccountId,
            AccountName = request.AccountName,
            Amount = request.Amount,
            CallbackUrl = "",
            ClientNotes = "",
            RedirectUrl = ""
        };

        if (transactionRequestId > 0)
        {
            var clientNotes = JsonSerializer.Serialize(new { TransactionId = transactionRequestId, UserAccount = _currentUserService.UserId.ToString() });

            depositRequest.ClientNotes = clientNotes;
            depositRequest.ClientTransactionId = $"GC-TRN{transactionRequestId.ToString().PadLeft(10, '0')}";
            depositRequest.CallbackUrl = $"{_baseCallback}/{transactionRequestId}";
        }

        var result = await _paymentApiService.CreateDepositToken(depositRequest, cancellationToken);

        return result;
    }
}
