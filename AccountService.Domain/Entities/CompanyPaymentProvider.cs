namespace AccountService.Domain.Entities;

public class CompanyPaymentProvider
{
    public int CompanyPaymentProviderId { get; set; }
    public Guid CompanyId { get; set; }
    public int PaymentProviderId { get; set; }

    public virtual PaymentProvider PaymentProvider { get; set; }
}