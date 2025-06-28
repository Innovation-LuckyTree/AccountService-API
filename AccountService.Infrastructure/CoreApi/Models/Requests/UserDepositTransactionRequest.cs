namespace AccountService.Infrastructure.CoreApi.Models.Requests;

public record UserDepositTransactionRequest(decimal Amount, int PaymentMethodId, string TransactionType, string Remarks);
