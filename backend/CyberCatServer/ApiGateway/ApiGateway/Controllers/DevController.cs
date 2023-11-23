using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.Server.Ids;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class DevController : ControllerBase
{
    private readonly IAuthService _authService;

    public DevController(IAuthService authService)
    {
        _authService = authService;
    }
}