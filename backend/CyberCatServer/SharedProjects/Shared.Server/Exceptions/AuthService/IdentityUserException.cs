using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace Shared.Server.Exceptions.AuthService
{
    [ProtoContract]
    public class IdentityUserException : ProtoExceptionModel
    {
        public IdentityUserException(string message) : base(message)
        {
        }

        public IdentityUserException()
        {
        }

        public override ActionResult ToActionResult()
        {
            return new ConflictObjectResult(Message);
        }
    }
}