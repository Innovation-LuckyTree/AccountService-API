using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.CashInAccount;

public class CashInAccountCommandHandler : IRequestHandler<CashInAccountCommand, AccountBalanceResponse>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public CashInAccountCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<AccountBalanceResponse> Handle(CashInAccountCommand request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreApiService.GetCurrentAccount(cancellationToken);

        var debitRequest = new AddDebitTransactionRequest(accountInfo.AccountObjectId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_CASH_IN,
            request.ModeOfTransaction, request.Amount, request.Notes);

        await _walletApiService.AddDebitTransactionRequest(debitRequest, cancellationToken);
        
        return await _walletApiService.GetAccountWalletBalance(accountInfo.AccountObjectId, cancellationToken);
    }
}