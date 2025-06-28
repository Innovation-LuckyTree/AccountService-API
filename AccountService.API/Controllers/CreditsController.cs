using AccountService.Application.Requests.AccountTransactions.Commands.AddBet;
using AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;
using AccountService.Application.Requests.Credits.Commands.TransferCreditToWallet;
using AccountService.Application.Requests.Credits.Commands.TransferWalletToCredit;
using AccountService.Application.Requests.Credits.Queries.GetAccountCreditTransactions;
using AccountService.Application.Requests.Credits.Queries.GetCreditBalance;
using AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactions;
using AccountService.Application.Requests.Credits.Queries.GetPagedAccountCreditTransactionsList;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

public class CreditsController : AuthorizedApiControllerBase
{
    [HttpGet("account")]
    public async Task<IActionResult> GetAccountCredits(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCreditBalanceQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("bet")]
    public async Task<IActionResult> AddBet([FromBody] AddBetCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("transfer/credit-wallet")]
    public async Task<IActionResult> TransferCreditToWallet([FromBody] TransferCreditToWalletCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("transfer/wallet-credit")]
    public async Task<IActionResult> PostCashIn([FromBody] TransferWalletToCreditCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetAccountTransactions(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountCreditTransactionsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPost("transactions/search")]
    public async Task<IActionResult> SearchTransactions(GetPagedAccountCreditTransactionsQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("transactions/search/{accountId}")]
    public async Task<IActionResult> SearchTransactions(Guid accountId, [FromBody]GetPagedAccountCreditTransactionsListQuery command, CancellationToken cancellationToken)
    {
        command.AccountId = accountId;
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }    

    [HttpPost("account")]
    public async Task<IActionResult> AddWalletToCreditAccount([FromBody] AddWalletToAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
