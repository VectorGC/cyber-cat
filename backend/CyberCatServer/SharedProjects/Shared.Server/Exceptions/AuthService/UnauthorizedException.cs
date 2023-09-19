using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace Shared.Server.Exceptions.AuthService;

[ProtoContract]
public class UnauthorizedException : ProtoExceptionModel
{
    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException()
    {
    }

    public override ActionResult ToActionResult()
    {
        return new ForbidResult();
    }
}