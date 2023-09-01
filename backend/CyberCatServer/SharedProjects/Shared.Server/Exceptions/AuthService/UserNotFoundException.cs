using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Models.Dto.ProtoHelpers;

namespace Shared.Server.Exceptions.AuthService;

[ProtoContract]
public class UserNotFoundException : ProtoExceptionModel
{
    private readonly string _email;

    public UserNotFoundException(string email) : base($"User with email '{email}' not found")
    {
        _email = email;
    }

    public UserNotFoundException()
    {
    }

    public override ActionResult ToActionResult()
    {
        return new NotFoundObjectResult(_email);
    }
}