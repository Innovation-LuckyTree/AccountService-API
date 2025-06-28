namespace AccountService.Infrastructure.WalletApi.Models.Requests;

public record AddCreditTransactionRequest(Guid AccountId, string AccountType, string TransactionNo, string TransactionReference, decimal Amount, string ModeOfTransaction, string? Notes);
