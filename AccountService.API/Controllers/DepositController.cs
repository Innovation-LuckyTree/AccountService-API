using AccountService.Application.Requests.PaymentProviders.Commands.CreateDepositToken;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers;

public class DepositController : AuthorizedApiControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> DepositToken(CreateDepositTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
