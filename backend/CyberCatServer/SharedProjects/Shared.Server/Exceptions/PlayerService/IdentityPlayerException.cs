using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Models.Dto.ProtoHelpers;

namespace Shared.Server.Exceptions;

[ProtoContract]
public class IdentityPlayerException : ProtoExceptionModel
{
    public IdentityPlayerException(string message) : base(message)
    {
    }

    public IdentityPlayerException()
    {
    }

    public override ActionResult ToActionResult()
    {
        return new ConflictObjectResult(Message);
    }
}