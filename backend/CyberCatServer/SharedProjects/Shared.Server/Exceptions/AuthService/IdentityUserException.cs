using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Models.Dto.ProtoHelpers;

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