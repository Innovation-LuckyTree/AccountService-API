using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.AccountTransactions.Commands.AddBet;

public class AddBetCommandHandler(IWalletApiService walletApiService) : IRequestHandler<AddBetCommand, AccountBalanceResponse>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<AccountBalanceResponse> Handle(AddBetCommand request, CancellationToken cancellationToken)
    {
        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            throw new Exception("Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var creditRequest = new AddCreditTransactionRequest(request.AccountId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_BET,
            amount, "", request.Notes);

        await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

        return await _walletApiService.GetAccountWalletBalance(request.AccountId, cancellationToken);
    }
}