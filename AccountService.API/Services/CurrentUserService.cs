using AccountService.Common.Interfaces;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AccountService.API.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext == null)
            return;

        var nameIdentifier = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues result))
        {
            if (result.Count > 0)
            {
                AuthenticationBearer = result[0].Replace("Bearer ", "");

                // parse jwt
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(AuthenticationBearer);
                var tokenObject = jsonToken as JwtSecurityToken;

                var companyId = tokenObject?.Claims.First(c => c.Type == "companyId").Value;
                if (Guid.TryParse(companyId, out Guid guidResult))
                {
                    CompanyId = guidResult;
                }
            }
        }

        if (string.IsNullOrEmpty(nameIdentifier))
        {
            return;
        }

        UserId = nameIdentifier;

    }

    public string UserId { get; }
    public string AuthenticationBearer { get; }
    public Guid CompanyId { get; }
}

