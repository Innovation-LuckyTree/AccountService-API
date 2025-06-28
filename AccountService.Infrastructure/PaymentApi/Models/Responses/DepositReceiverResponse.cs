namespace AccountService.Infrastructure.PaymentApi.Models.Responses;

public class DepositReceiverResponse
{
    public string AccountNumber { get; set; }
    public string AccountQR { get; set; }
    public string ReferenceCode { get; set; }

}