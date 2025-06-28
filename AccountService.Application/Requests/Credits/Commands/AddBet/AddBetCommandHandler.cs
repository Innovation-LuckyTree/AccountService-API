using AccountService.Application.Common.Constants;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Commands.AddBet;

public class AddBetCommandHandler : IRequestHandler<AddBetCommand, AccountBalanceResponse>
{
    private readonly ICoreApiService _coreApiService;
    private readonly IWalletApiService _walletApiService;

    public AddBetCommandHandler(ICoreApiService coreApiService, IWalletApiService walletApiService)
    {
        _coreApiService = coreApiService;
        _walletApiService = walletApiService;
    }

    public async Task<AccountBalanceResponse> Handle(AddBetCommand request, CancellationToken cancellationToken)
    {

        var currentWalletBalance = await _walletApiService.GetAccountWalletBalance(request.AccountCreditId, cancellationToken);

        if (request.Amount > currentWalletBalance.Balance)
        {
            throw new Exception("Account Insuficient Balance!");
        }
        var amount = request.Amount > 0 ? request.Amount * -1 : request.Amount;

        var creditRequest = new AddCreditTransactionRequest(request.AccountCreditId, 
            AccountTypes.ACCOUNT_PLAYER, request.TransactionNo, TransactionReferenceTypes.ACCOUNT_BET,
            amount, "", request.Notes);

        await _walletApiService.AddCreditTransactionRequest(creditRequest, cancellationToken);

        return await _walletApiService.GetAccountWalletBalance(request.AccountCreditId, cancellationToken);
    }
}