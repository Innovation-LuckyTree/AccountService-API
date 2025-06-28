using AccountService.Application.Common.Constants;
using AccountService.Application.Common.Interfaces;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Commands.TransferWalletToCredit;

public class TransferWalletToCreditCommandHandler : IRequestHandler<TransferWalletToCreditCommand, BaseResponse<AccountBalanceResponse>>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public TransferWalletToCreditCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<BaseResponse<AccountBalanceResponse>> Handle(TransferWalletToCreditCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AccountBalanceResponse>();

        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(request.AccountWalletId, cancellationToken);

        // Check if credit account have enough balance
        if (request.Amount > currentWalletBalance.Balance)
        {
            response.Status = "failed";
            response.ErrorMessage = "Account Insuficient Balance!";

            return response;
        }

        // Convert to negative
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        // Add credit transaction to wallet account
        var creditRequest = new AddCreditTransactionRequest(request.AccountWalletId,
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.CREDIT_TO_WALLET_TRANSFER,
            amount, "", request.Notes);

        await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);


        // Add Debit to credit account
        var debitRequest = new AddDebitTransactionRequest(request.AccountCreditId,
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.WALLET_TO_CREDIT_TRANSFER,
            request.ModeOfTransaction, request.Amount, request.Notes);

        await _walletApiService.AddDebitTransactionRequest(debitRequest, cancellationToken);

        var result = await _walletApiService.GetAccountWalletBalance(request.AccountWalletId, cancellationToken);
        response.Data = result;

        return response;
    }
}