namespace AccountService.Infrastructure.WalletApi.Models.Requests;

public record AddDebitTransactionRequest(Guid AccountId, string AccountType, string TransactionNo, string TransactionReference, string ModeOfTransaction, decimal Amount, string Notes);