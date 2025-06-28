using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.PaymentProviders.Commands.RbgiWithdrawAccountBalance;


public class RbgiWithdrawAccountBalanceCommandHandler(IPaymentApiService paymentApiService) : IRequestHandler<RbgiWithdrawAccountBalanceCommand, RbgiWithdrawData>
{
    private readonly IPaymentApiService _paymentApiService = paymentApiService;
    private readonly string _gcashBankcode = "GXCHPHM2XXX";

    public async Task<RbgiWithdrawData> Handle(RbgiWithdrawAccountBalanceCommand request, CancellationToken cancellationToken)
    {        
        var depositRequest = new RbgiWithdrawRequest(request.AccountObjectId, request.AccountName, request.AccountNumber, _gcashBankcode, request.Amount.ToString(), request.TransactionId);

        var result = await _paymentApiService.RbgiWithdrawAccount(depositRequest, cancellationToken);

        return result;
    }
}

