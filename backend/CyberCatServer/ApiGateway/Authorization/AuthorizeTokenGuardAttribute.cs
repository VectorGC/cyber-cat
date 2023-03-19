using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Authorization;

public class AuthorizeTokenGuardAttribute : TypeFilterAttribute
{
    public AuthorizeTokenGuardAttribute() : base(typeof(AuthorizeTokenGuard))
    {
    }
}