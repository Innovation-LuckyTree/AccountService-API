using MediatR;

namespace AccountService.Application.Requests.Transactions.Queries.SearchTransactionByCompany;

public class SearchTransactionByCompanyQuery : IRequest<TransactionVm>
{
    public string Type { get; set; }
    public Guid? UserAccountId { get; set; }
    public string TransactionId { get; set; }
}
