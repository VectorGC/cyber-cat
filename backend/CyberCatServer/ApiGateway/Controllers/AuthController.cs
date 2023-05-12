using ApiGateway.Dto;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf.Grpc.Client;
using Shared.Dto;
using Shared.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("get")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Get()
    {
        return Ok("Ok!");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string username, string password)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:6001");
        var service = channel.CreateGrpcService<IAuthService>();
        var access = await service.GetAccessToken(new GetAccessTokenArgsDto
        {
            Email = username,
            Password = password
        });

        var response = new LoginResponseDto
        {
            AccessToken = access.Value
        };

        return Ok(response);
    }
}