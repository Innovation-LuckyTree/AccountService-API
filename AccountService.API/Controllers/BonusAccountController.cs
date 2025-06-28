using AccountService.Application.Requests.BonusAccountTransactions.Commands.AddBetByBonus;
using AccountService.Application.Requests.BonusAccountTransactions.Commands.AddWalletToBonusAccount;
using AccountService.Application.Requests.BonusAccountTransactions.Commands.CreditBonusAccountBalance;
using AccountService.Application.Requests.BonusAccountTransactions.Commands.ProcessReturnBonus;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusAccountCredits;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByAccount;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetBonusTransactionByPromotion;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetPagedBonusWalletTransactions;
using AccountService.Application.Requests.BonusAccountTransactions.Queries.GetUserBonusCredits;
using AccountService.Infrastructure.WalletApi.Models.Requests.BonusAccounts;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

[Route("api/bonus-account")]
public class BonusAccountController : AuthorizedApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddWalletToBonusAccount([FromBody] AddWalletToBonusAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("bet")]
    public async Task<IActionResult> AddBet([FromBody] AddBetByBonusCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("credits")]
    public async Task<IActionResult> GetAccountCredits(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBonusAccountCreditsQuery(), cancellationToken);

        return Ok(result);
    }

    //[HttpGet("transactions")]
    //public async Task<IActionResult> GetAccountTransactions(CancellationToken cancellationToken)
    //{
    //    var result = await Mediator.Send(new GetBonusAccountWalletTransactionsQuery(), cancellationToken);

    //    return Ok(result);
    //}

    [HttpGet("transactions/{accountId}")]
    public async Task<IActionResult> GetAccountTransactionsById(Guid accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBonusTransactionByAccountQuery(accountId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("transactions/search")]
    public async Task<IActionResult> SearchTransactions(GetPagedBonusWalletTransactionsQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("transactions/promotion")]
    public async Task<IActionResult> GetBonusAccountTransactionByPromotion(BonusAccountByPromotionRequest query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBonusTransactionByPromotionQuery(query), cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("process/return")]
    public async Task<IActionResult> ProcessReturnBonusTransaction(ProcessReturnBonusCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return Ok(result);
    }


    [HttpPost("credit")]
    public async Task<IActionResult> CreditAccountBalanceByAccount([FromBody] CreditBonusAccountBalanceCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("credits/{id}")]
    public async Task<IActionResult> GetUserAccountCredits(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserBonusCreditsQuery(id), cancellationToken);

        return Ok(result);
    }
}
