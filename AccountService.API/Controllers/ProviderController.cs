using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountCredits;
using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactions;
using AccountService.Application.Requests.PaymentProviders.Commands.CreateAccount;
using AccountService.Infrastructure.Clients.ConnectPay.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

public class ProviderController : AuthorizedApiControllerBase
{
    [HttpGet("account")]
    public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountCreditsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("account/{id}")]
    public async Task<IActionResult> GetAccountById(string id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountWalletTransactionsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("account")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new CreateAccountCommand(request), cancellationToken);

        return Ok(result);
    }


}
