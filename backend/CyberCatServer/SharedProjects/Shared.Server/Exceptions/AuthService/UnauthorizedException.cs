using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Models.Dto.ProtoHelpers;

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