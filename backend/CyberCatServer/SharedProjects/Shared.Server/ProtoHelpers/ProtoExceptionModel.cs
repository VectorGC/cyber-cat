using System;
using Microsoft.AspNetCore.Mvc;
using ProtoBuf;

namespace Shared.Server.ProtoHelpers
{
    [ProtoContract]
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