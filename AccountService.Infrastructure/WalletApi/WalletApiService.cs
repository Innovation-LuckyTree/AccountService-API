using System.Net.Http.Json;
using AccountService.Infrastructure.Helpers;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.WalletApi.Models.Requests;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using AccountService.Infrastructure.WalletApi.Models.Responses;
using AccountService.Infrastructure.WalletApi.Models.Responses.BonusAccounts;

namespace AccountService.Infrastructure.WalletApi;

public class WalletApiService : AbstractApiClient, IWalletApiService
{
    private readonly IAppConfig _appConfig;

    public WalletApiService(HttpClient? client, IAppConfig appConfig) : base(nameof(WalletApiService), client)
    {
        _client.BaseAddress = new Uri(appConfig.WalletApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.WalletApiClient.Resource);

        _appConfig = appConfig;
    }

    #region Wallet Account
    public async Task<T> GetAccountWalletTransaction<T>(Guid accountId, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.GetAsync($"/api/account/{accountId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<T> GetAccountWalletTransaction<T>(PagedAccountTransactionRequest request, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.PostAsJsonAsync("/api/Account/transaction/search", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<AccountBalanceResponse> GetAccountWalletBalance(Guid accountId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/account/balance/{accountId.ToString()}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountBalanceResponse>();
        return content!;
    }

    public async Task AddCreditTransactionRequest(AddCreditTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/account/credit", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task AddDebitTransactionRequest(AddDebitTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/account/debit", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<WalletBalancesResponse> GetWalletBalances(WalletBalancesRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/account/balances", request, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadFromJsonAsync<WalletBalancesResponse>();
        return content!;
    }
    #endregion

    #region Bonus Account
    public async Task<T> GetBonusAccountTransaction<T>(Guid accountId, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.GetAsync($"/api/bonus-account/{accountId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<T> GetBonusAccountWalletTransaction<T>(PagedBonusTransactionRequest request, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.PostAsJsonAsync("/api/bonus-account/transaction/search", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<T> GetBonusAccountTransactionsByPromotion<T>(BonusAccountByPromotionRequest request, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.PostAsJsonAsync("/api/bonus-account/transaction/promotion", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<BonusAccountBalanceResponse> GetBonusAccountBalance(Guid accountId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/bonus-account/balance/{accountId.ToString()}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new BonusAccountBalanceResponse();
        }

        var content = await response.Content.ReadFromJsonAsync<BonusAccountBalanceResponse>();
        return content!;
    }

    public async Task AddBonusCreditTransactionRequest(AddBonusCreditTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/bonus-account/credit", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task AddBonusDebitTransactionRequest(AddBonusDebitTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/bonus-account/debit", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
    #endregion
}
