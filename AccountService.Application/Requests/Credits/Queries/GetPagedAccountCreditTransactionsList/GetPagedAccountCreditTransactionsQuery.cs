using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactionsList;

public class GetPagedAccountCreditTransactionsListQuery : IRequest<AccountDto>
{
    public Guid AccountId { get; set; }
    public string SearchKey { get; set; }
    public int? TransactionType { get; set; } //0-credit , 1-debit
    public int Start { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public DateTime? StartDate { get; set; } = DateTime.Now.AddDays(-7);
    public DateTime? EndDate { get; set; } = DateTime.Now;
}
