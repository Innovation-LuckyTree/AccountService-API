using AccountService.Infrastructure.WalletApi.Models.Responses;
using MediatR;

namespace AccountService.Application.Requests.Credits.Queries.GetCreditBalance;

public class GetCreditBalanceQuery : IRequest<AccountBalanceResponse>
{
}
