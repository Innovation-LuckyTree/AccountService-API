using AccountService.Infrastructure.CoreApi.Models.Requests;
using AccountService.Infrastructure.CoreApi.Models.Responses;

namespace AccountService.Infrastructure.Interfaces;

public interface ICoreApiService
{
    Task<AccountInfo> GetCurrentAccount(CancellationToken cancellationToken);
    Task<AccountInfo> GetAccountInfoByPaymentAccount(string paymentAccountId, CancellationToken cancellationToken);
    Task<AccountInfo> GetAccountInfoByUserId(Guid userId, CancellationToken cancellationToken);
    Task<AccountInfo> GetAccountByAccountObjectId(Guid accountObjectId, CancellationToken cancellationToken);
    Task<AccountInfo> GetUserByMobile(string mobileNumber, CancellationToken cancellationToken);
    Task<T> GetAccountInfo<T>(Guid AccountId, CancellationToken cancellationToken) where T : class;
    Task SaveUserDepositTransaction(UserDepositTransactionRequest request, CancellationToken cancellationToken);
}
