using AccountService.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Requests.TransactionRequests.Queries.GetTransactionRequestById;

public class GetTransactionRequestByIdQueryHandler : IRequestHandler<GetTransactionRequestByIdQuery, TransactionRequestDto>
{
    private readonly IPaymentDataSourceDbContext _paymentDataSourceDbContext;
    private readonly IMapper _mapper;

    public GetTransactionRequestByIdQueryHandler(IPaymentDataSourceDbContext paymentDataSourceDbContext, IMapper mapper)
    {
        _paymentDataSourceDbContext = paymentDataSourceDbContext;
        _mapper = mapper;
    }

    public async Task<TransactionRequestDto> Handle(GetTransactionRequestByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _paymentDataSourceDbContext.TransactionRequests.Where(o => o.TransactionRequestId == request.Id)
            .ProjectTo<TransactionRequestDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}