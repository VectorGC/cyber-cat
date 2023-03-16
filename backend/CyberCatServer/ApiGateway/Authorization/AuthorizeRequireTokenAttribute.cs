using ApiGateway.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Authorization;

public class AuthorizeRequireTokenAttribute : TypeFilterAttribute
{
    public AuthorizeRequireTokenAttribute() : base(typeof(AuthorizeRequireToken))
    {
    }
}