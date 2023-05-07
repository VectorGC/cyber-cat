using AuthService.Dto;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Controller]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(UserManager<User> userManager, JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpGet("get")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Get()
    {
        return Ok("Ok!");
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByEmailAsync(request.Email);
        if (managedUser == null)
        {
            return BadRequest("Bad credentials");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Bad credentials");
        }

        var accessToken = _jwtTokenService.CreateToken(managedUser);
        await _userManager.SetAuthenticationTokenAsync(managedUser, JwtBearerDefaults.AuthenticationScheme, "access", accessToken);

        return Ok(new AuthResponse
        {
            Username = managedUser.UserName,
            Email = managedUser.Email,
            Token = accessToken,
        });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        if (ModelState.IsValid)
        {
            User appUser = new User
            {
                UserName = createUserDto.Name,
                Email = createUserDto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, createUserDto.Password);
            if (result.Succeeded)
                return Ok("User Created Successfully");
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return Ok(result);
            }
        }

        return Ok(createUserDto);
    }
}