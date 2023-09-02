using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace Shared.Server.Exceptions.PlayerService;

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