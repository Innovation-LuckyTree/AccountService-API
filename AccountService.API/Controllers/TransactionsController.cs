using AccountService.Application.Requests.Transactions.Commands.CreateTransaction;
using AccountService.Application.Requests.Transactions.Commands.ProcessTransaction;
using AccountService.Application.Requests.Transactions.Commands.UpdateTransaction;
using AccountService.Application.Requests.Transactions.Queries.GetPendingNotification;
using AccountService.Application.Requests.Transactions.Queries.GetTransactionNotification;
using AccountService.Application.Requests.Transactions.Queries.GetUnprocessedTransaction;
using AccountService.Application.Requests.Transactions.Queries.SearchTransactionByCompany;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

public class TransactionsController : AuthorizedApiControllerBase
{
    /// <summary>
    /// Create transaction to wallet and payment
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    /// <summary>
    /// Search Transaction
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<IActionResult> SearchTransaction([FromBody] SearchTransactionByCompanyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingTransactions(CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(new GetUnprocessedTransactionQuery(), cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessTransaction(ProcessTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpGet("notification/pending")]
    public async Task<IActionResult> GetPendingTransactionNotification(CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(new GetPendingNotificationQuery(), cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpGet("account/notification/{transactionId}")]
    public async Task<IActionResult> TransactionNotification(long transactionId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(new GetTransactionNotificationQuery(transactionId), cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpPatch("notification/{transactionId}")]
    public async Task<IActionResult> Update(long transactionId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(new UpdateTransactionCommand(transactionId) { Notified = true }, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> CreateDepositRequest([FromBody] CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> CreateWithdrawRequest([FromBody] CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await Mediator.Send(request, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.ToString() });
        }
    }
}
