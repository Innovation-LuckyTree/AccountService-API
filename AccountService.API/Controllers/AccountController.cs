using AccountService.Application.Requests.Accounts.Queries.GetAccountListBalance;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    public class AccountController : AuthorizedApiControllerBase
    {
        [HttpPost("balances")]
        public async Task<IActionResult> GetAccountBalances(GetAccountListBalanceQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
