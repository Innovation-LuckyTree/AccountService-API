namespace AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;

public record RbgiWithdrawRequest(Guid AccountObjectId, string ClientAccountName, string ClientAccountNo, string TransferBankCode, string TransactionAmount, string TransactionId)
{
}
