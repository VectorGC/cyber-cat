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
    private readonly IAuthGrpcService _authGrpcService;

    public DevController(IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
    }

    [HttpPost("users/remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> RemoveDev(string email, string devKey)
    {
        var key = await Crypto.DecryptAsync(devKey, "cyber");
        if (key != "cyber-cat")
        {
            return Ok();
        }

        return await _authGrpcService.RemoveDev(new RemoveDevArgs(email));
    }
}