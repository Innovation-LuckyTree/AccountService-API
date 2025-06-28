using System.Net.Http.Json;
using System.Text.Json;
using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.Helpers;
using AccountService.Infrastructure.Interfaces;
using AccountService.Infrastructure.PaymentApi.Models.Responses;
using Microsoft.Extensions.Logging;

namespace AccountService.Infrastructure.PaymentApi;

public class PaymentApiService : AbstractApiClient, IPaymentApiService
{
    private readonly IAppConfig _appConfig;
    private readonly ILogger<PaymentApiService> _logger;

    public PaymentApiService(HttpClient? client, IAppConfig appConfig, ILogger<PaymentApiService> logger) : base(nameof(PaymentApiService), client)
    {
        _client.BaseAddress = new Uri(appConfig.PaymentApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.PaymentApiClient.Resource);

        _appConfig = appConfig;
        _logger = logger;

    }

    #region Account
    public async Task<AvailabilityResponse> GetAvailability(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/api/auth/availability", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<AvailabilityResponse>(cancellationToken);

        return result!;
    }

    public async Task<AccountListResponse> GetAccounts(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/api/account", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<AccountListResponse>(cancellationToken);

        return result!;
    }

    public async Task<AccountResponse> GetAccountById(string accountId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/account/{accountId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<AccountResponse>(cancellationToken);

        return result!;
    }

    public async Task<AccountResponse> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/account", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<AccountResponse>(cancellationToken);

        return result!;
    }
    #endregion

    #region Deposit
    public async Task<DepositResponse> CreateDeposit(DepositRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/deposit", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<DepositResponse>(cancellationToken);

        return result!;
    }

    public async Task<DepositTokenResponse> CreateDepositToken(DepositTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/deposit/token", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<DepositTokenResponse>(cancellationToken);

        return result!;
    }
    #endregion

    #region Transaction
    public async Task<TransactionListResponse> GetTransactions(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/api/transction", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<TransactionListResponse>(cancellationToken);

        return result!;
    }

    public async Task<TransactionResponse> GetTransactionById(string transactionId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/transaction/{transactionId}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<TransactionResponse>(cancellationToken);

        return result!;
    }
    #endregion

    #region Withdraw
    public async Task<WithdrawResponse> WithdrawAccount(WithdrawRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/withdraw", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"Failed to add account {response.StatusCode}");
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<WithdrawResponse>(cancellationToken);

        return result!;
    }

    public async Task<RbgiWithdrawData> RbgiWithdrawAccount(RbgiWithdrawRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/provider/outward", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            _logger.LogError($"Failed to add account {response.StatusCode}", errResponse);
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<RbgiWithdrawData>(cancellationToken);

        return result!;
    }
    #endregion
}
