using AccountService.Application.Common.Constants;
using AccountService.Application.Common.Interfaces;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Commands.TransferCreditToWallet;

public class TransferCreditToWalletCommandHandler : IRequestHandler<TransferCreditToWalletCommand, BaseResponse<AccountBalanceResponse>>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public TransferCreditToWalletCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<BaseResponse<AccountBalanceResponse>> Handle(TransferCreditToWalletCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<AccountBalanceResponse>();

        var currentCreditBalance = await _walletApiService.GetAccountWalletBalance(request.AccountCreditId, cancellationToken);

        // Check if credit account have enough balance
        if (request.Amount > currentCreditBalance.Balance)
        {
            response.Status = "failed";
            response.ErrorMessage = "Account Insuficient Balance!";

            return response;
        }

        // Convert to negative
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        // Add credit transaction to credit account
        var creditRequest = new AddCreditTransactionRequest(request.AccountCreditId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.CREDIT_TO_WALLET_TRANSFER,
            amount, "", request.Notes);

        await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

        // Add debit to wallet account
        var debitRequest = new AddDebitTransactionRequest(request.AccountWalletId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.WALLET_TO_CREDIT_TRANSFER,
            request.ModeOfTransaction, request.Amount, request.Notes);

        await _walletApiService.AddDebitTransactionRequest(debitRequest, cancellationToken);

        var result = await _walletApiService.GetAccountWalletBalance(request.AccountCreditId, cancellationToken);
        response.Data = result;

        return response;
    }
}