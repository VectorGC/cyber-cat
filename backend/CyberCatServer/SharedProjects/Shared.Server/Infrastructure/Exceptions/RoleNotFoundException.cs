using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Models.Domain.Users;
using Shared.Server.Exceptions;

namespace Shared.Server.Infrastructure.Exceptions;

[ProtoContract(SkipConstructor = true)]
public class RoleNotFoundException : ProtoExceptionModel
{
    private readonly string _name;

    public RoleNotFoundException(Role role) : base($"Role with name '{role.Id}' not found")
    {
        _name = role.Id;
    }

    public override ActionResult ToActionResult()
    {
        return new NotFoundObjectResult(_name);
    }
}