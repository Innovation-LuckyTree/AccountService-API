using AccountService.Application.Requests.AccountTransactions.Commands.AddWalletToAccount;
using AccountService.Application.Requests.AccountTransactions.Commands.CashInAccount;
using AccountService.Application.Requests.AccountTransactions.Commands.ProcessWithdrawAccount;
using AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalance;
using AccountService.Application.Requests.AccountTransactions.Commands.WithdrawAccountBalanceByAccount;
using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountCredits;
using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactions;
using AccountService.Application.Requests.AccountTransactions.Queries.GetAccountWalletTransactionsByAccount;
using AccountService.Application.Requests.AccountTransactions.Queries.GetCurrentTotalTransaction;
using AccountService.Application.Requests.AccountTransactions.Queries.GetPagedAccountWalletTransactions;
using AccountService.Application.Requests.AccountTransactions.Queries.GetUserAccountCredits;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

public class AccountTransactionController : AuthorizedApiControllerBase
{
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentBalances(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentTotalTransactionQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("credits")]
    public async Task<IActionResult> GetAccountCredits(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountCreditsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetAccountTransactions(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountWalletTransactionsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("transactions/{accountId}")]
    public async Task<IActionResult> GetAccountTransactionsById(Guid accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountWalletTransactionsByAccountQuery(accountId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("transactions/search")]
    public async Task<IActionResult> SearchTransactions(GetPagedAccountWalletTransactionsQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("cash-in")]
    public async Task<IActionResult> PostCashIn([FromBody] CashInAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawAccountBalance([FromBody] WithdrawAccountBalanceCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    // Withdraw account using GCash
    [HttpPost("account/withdraw")]
    public async Task<IActionResult> WithdrawAccountBalanceByAccount([FromBody] WithdrawAccountBalanceByAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    //Withdraw account balance directly
    [HttpPost("balance/withdraw")]
    public async Task<IActionResult> ProcessWithdrawAccount([FromBody] ProcessWithdrawAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("credits/{id}")]
    // [Authorize(Roles = "")]
    public async Task<IActionResult> GetUserAccountCredits(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserAccountCreditsQuery(id), cancellationToken);

        return Ok(result);
    }


    [HttpPost("account/wallet")]
    public async Task<IActionResult> AddWalletToAccount([FromBody] AddWalletToAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("account/credit")]
    public async Task<IActionResult> AddWalletToCreditAccount([FromBody] AddWalletToAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
