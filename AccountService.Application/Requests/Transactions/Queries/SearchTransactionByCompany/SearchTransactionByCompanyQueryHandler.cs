using AccountService.Application.Common.Interfaces;
using AccountService.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.Transactions.Queries.SearchTransactionByCompany;

public class SearchTransactionByCompanyQueryHandler : IRequestHandler<SearchTransactionByCompanyQuery, TransactionVm>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public SearchTransactionByCompanyQueryHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _paymentDataSourceDbContext = paymentDataSourceDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<TransactionVm> Handle(SearchTransactionByCompanyQuery request, CancellationToken cancellationToken)
    {
        var query = _paymentDataSourceDbContext.Transactions
            .Include(o => o.PaymentProvider)
            .Where(o => o.CompanyId == _currentUserService.CompanyId);

        if (!string.IsNullOrEmpty(request.Type))
        {
            query = query.Where(o => o.Type == request.Type);
        }

        if (request.UserAccountId.HasValue)
        {
            query = query.Where(o => o.UserAccountId == request.UserAccountId);
        }

        if (!string.IsNullOrEmpty(request.TransactionId))
        {
            query = query.Where(o => o.ResponseId == request.TransactionId);
        }

        var transactions = await query.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new TransactionVm(transactions);
    }
}