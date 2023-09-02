using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Models;

namespace Shared.Server.Exceptions.PlayerService;

[ProtoContract]
public class PlayerNotFoundException : ProtoExceptionModel
{
    public PlayerNotFoundException(PlayerId playerId) : base($"Player '{playerId}' not found")
    {
    }

    public PlayerNotFoundException(UserId userId) : base($"Player for user '{userId}' not found")
    {
    }

    public PlayerNotFoundException()
    {
    }

    public override ActionResult ToActionResult()
    {
        return new NotFoundObjectResult(Message);
    }
}