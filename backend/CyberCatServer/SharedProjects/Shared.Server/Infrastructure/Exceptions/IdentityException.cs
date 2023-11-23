using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.Infrastructure.Exceptions
{
    [ProtoContract]
    public class IdentityException : ProtoExceptionModel
    {
        public IdentityException(string message) : base(message)
        {
        }

        public IdentityException()
        {
        }

        public override ActionResult ToActionResult()
        {
            return new ConflictObjectResult(Message);
        }
    }
}