namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class TransactionResponse : ApiResponse<TransactionData>
{

}

public class TransactionData
{
    public string Id { get; set; }
    public string AccountId { get; set; }

    public string Type { get; set; }
    public string Status { get; set; }
    public string StatusNotes { get; set; }
    public decimal Amount { get; set; }
    public string AccountName { get; set; }
    public string AccountNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public DepositReceiverResponse Reciever { get; set; }
}