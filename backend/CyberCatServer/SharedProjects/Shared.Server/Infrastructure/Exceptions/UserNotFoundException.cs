using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.Infrastructure.Exceptions;

[ProtoContract(SkipConstructor = true)]
public class UserNotFoundException : ProtoExceptionModel
{
    private readonly string _email;

    public UserNotFoundException(string email) : base($"User with email '{email}' not found")
    {
        _email = email;
    }

    public override ActionResult ToActionResult()
    {
        return new NotFoundObjectResult(_email);
    }
}