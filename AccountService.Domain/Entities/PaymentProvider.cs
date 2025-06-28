namespace AccountService.Domain.Entities;

public class PaymentProvider
{
    public int PaymentProviderId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Configuration { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual IEnumerable<CompanyPaymentProvider> CompanyPaymentProviders { get; set; }
    public virtual IEnumerable<Transaction> Transactions { get; set; }
    public virtual IEnumerable<Deposit> Deposits { get; set; }
    public virtual IEnumerable<DepositToken> DepositTokens { get; set; }
    public virtual IEnumerable<Withdrawal> Withdrawals { get; set; }
}