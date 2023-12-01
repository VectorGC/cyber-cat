using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.Infrastructure.Exceptions;

[ProtoContract(SkipConstructor = true)]
public class RoleNotFoundException : ProtoExceptionModel
{
    private readonly string _name;

    public RoleNotFoundException(string role) : base($"Role with name '{role}' not found")
    {
        _name = role;
    }

    public override ActionResult ToActionResult()
    {
        return new NotFoundObjectResult(_name);
    }
}