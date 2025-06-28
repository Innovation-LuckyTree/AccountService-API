using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

namespace AccountService.Infrastructure.Interfaces;

public interface IWalletApiService
{
    Task<T> GetAccountWalletTransaction<T>(Guid accountId, CancellationToken cancellationToken) where T : class;
    Task<AccountBalanceResponse> GetAccountWalletBalance(Guid accountId, CancellationToken cancellationToken);
    Task<T> GetAccountWalletTransaction<T>(PagedAccountTransactionRequest request, CancellationToken cancellationToken) where T : class;
    Task AddCreditTransactionRequest(AddCreditTransactionRequest request, CancellationToken cancellationToken);
    Task AddDebitTransactionRequest(AddDebitTransactionRequest request, CancellationToken cancellationToken);
    Task<WalletBalancesResponse> GetWalletBalances(WalletBalancesRequest request, CancellationToken cancellationToken);
    Task<T> GetBonusAccountTransactionsByPromotion<T>(BonusAccountByPromotionRequest request, CancellationToken cancellationToken) where T : class;
    Task<T> GetBonusAccountTransaction<T>(Guid accountId, CancellationToken cancellationToken) where T : class;
    Task<T> GetBonusAccountWalletTransaction<T>(PagedBonusTransactionRequest request, CancellationToken cancellationToken) where T : class;
    Task<BonusAccountBalanceResponse> GetBonusAccountBalance(Guid accountId, CancellationToken cancellationToken);
    Task AddBonusCreditTransactionRequest(AddBonusCreditTransactionRequest request, CancellationToken cancellationToken);
    Task AddBonusDebitTransactionRequest(AddBonusDebitTransactionRequest request, CancellationToken cancellationToken);
}
