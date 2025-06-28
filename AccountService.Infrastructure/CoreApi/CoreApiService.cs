using AccountService.Infrastructure.CoreApi.Models.Requests;
using AccountService.Infrastructure.CoreApi.Models.Responses;
using AccountService.Infrastructure.Helpers;
using AccountService.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;

namespace AccountService.Infrastructure.CoreApi;

public class CoreApiService : AbstractApiClient, ICoreApiService
{
    private readonly IAppConfig _appConfig;
    private readonly ILogger<CoreApiService> _logger;

    public CoreApiService(HttpClient? client, IAppConfig appConfig, ILogger<CoreApiService> logger) : base(nameof(CoreApiService), client)
    {
        _client.BaseAddress = new Uri(appConfig.CoreApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.CoreApiClient.Resource);

        _appConfig = appConfig;
        _logger = logger;

    }

    public async Task<AccountInfo> GetCurrentAccount(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/account/current/info", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account Details");

            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountInfo>();
        return content!;
    }

    public async Task<AccountInfo> GetAccountInfoByPaymentAccount(string paymentAccountId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/account/payment-account/{paymentAccountId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account Details");

            return null;
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountInfo>();
        return content!;
    }

    public async Task<AccountInfo> GetAccountInfoByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/account/user-info/{userId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account Details");

            return null;
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountInfo>();
        return content!;
    }

    public async Task<AccountInfo> GetAccountByAccountObjectId(Guid accountObjectId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/account/info/{accountObjectId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account Details");

            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountInfo>();
        return content!;
    }

    public async Task<AccountInfo> GetUserByMobile(string mobileNumber, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"api/account/details/{mobileNumber}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account Details");

            return null;
        }

        try
        {
            var content = await response.Content.ReadFromJsonAsync<AccountInfo>();
            return content!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<T> GetAccountInfo<T>(Guid AccountId, CancellationToken cancellationToken) where T : class
    {
        var response = await _client.GetAsync($"api/user/{AccountId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to Get Account info");
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    #region Account Deposit and Withdraw
    public async Task SaveUserDepositTransaction(UserDepositTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"api/deposit/user/transaction", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var reponseMessage = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(reponseMessage, "Failed to save User Deposit transaction");
        }
    }
    #endregion
}
