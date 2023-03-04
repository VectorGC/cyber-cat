using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserCollection _userCollection;

    public UserController(IUserService userService, IUserCollection userCollection)
    {
        _userCollection = userCollection;
        _userService = userService;
    }

    [HttpPut]
    public async Task<IActionResult> Add(UserApiDto userApi)
    {
        await _userService.Add(userApi);
        return Ok(userApi);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userCollection.GetAll();
        return Ok(users.Select(UserApiDto.FromUserDto));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IUserDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userCollection.Get(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}