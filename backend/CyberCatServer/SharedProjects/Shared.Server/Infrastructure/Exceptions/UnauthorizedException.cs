using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.Infrastructure.Exceptions;

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