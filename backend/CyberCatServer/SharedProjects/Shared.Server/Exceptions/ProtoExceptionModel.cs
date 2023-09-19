using System;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions.AuthService;
using Shared.Server.Exceptions.PlayerService;

namespace Shared.Server.Exceptions
{
    [ProtoContract]
    [ProtoInclude(100, typeof(UserNotFoundException))]
    [ProtoInclude(101, typeof(UnauthorizedException))]
    [ProtoInclude(102, typeof(IdentityUserException))]
    [ProtoInclude(103, typeof(PlayerNotFoundException))]
    [ProtoInclude(104, typeof(IdentityPlayerException))]
    public abstract class ProtoExceptionModel : Exception
    {
        [ProtoMember(1)] public string ProtoMessage { get; set; }

        public override string Message => ProtoMessage;

        public ProtoExceptionModel(string message)
        {
            ProtoMessage = message;
        }

        public ProtoExceptionModel()
        {
        }

        public abstract ActionResult ToActionResult();
    }
}