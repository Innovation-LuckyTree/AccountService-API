using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using AccountService.Infrastructure.PaymentApi.Models.Responses;

namespace AccountService.Infrastructure.Interfaces;
public interface IPaymentApiService
{
    Task<AvailabilityResponse> GetAvailability(CancellationToken cancellationToken);
    Task<AccountListResponse> GetAccounts(CancellationToken cancellationToken);
    Task<AccountResponse> GetAccountById(string accountId, CancellationToken cancellationToken);
    Task<AccountResponse> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken);
    Task<DepositResponse> CreateDeposit(DepositRequest request, CancellationToken cancellationToken);
    Task<DepositTokenResponse> CreateDepositToken(DepositTokenRequest request, CancellationToken cancellationToken);
    Task<TransactionListResponse> GetTransactions(CancellationToken cancellationToken);
    Task<TransactionResponse> GetTransactionById(string transactionId, CancellationToken cancellationToken);
    Task<WithdrawResponse> WithdrawAccount(WithdrawRequest request, CancellationToken cancellationToken);
    Task<RbgiWithdrawData> RbgiWithdrawAccount(RbgiWithdrawRequest request, CancellationToken cancellationToken);
}
