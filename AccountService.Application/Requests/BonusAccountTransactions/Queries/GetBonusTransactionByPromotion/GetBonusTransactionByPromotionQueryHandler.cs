using AccountService.Infrastructure.Interfaces;
using MediatR;

namespace AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByPromotion;

public class GetBonusTransactionByPromotionQueryHandler(IWalletApiService walletApiService) : IRequestHandler<GetBonusTransactionByPromotionQuery, BonusAccountTransactionPromotionVm>
{
    private readonly IWalletApiService _walletApiService = walletApiService;

    public async Task<BonusAccountTransactionPromotionVm> Handle(GetBonusTransactionByPromotionQuery request, CancellationToken cancellationToken)
        => await _walletApiService.GetBonusAccountTransactionsByPromotion<BonusAccountTransactionPromotionVm>(request.Data, cancellationToken);  
}