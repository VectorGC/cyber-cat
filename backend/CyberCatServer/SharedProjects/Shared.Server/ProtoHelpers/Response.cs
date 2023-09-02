using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.ProtoHelpers
{
    [ProtoContract]
    public class Response
    {
        [ProtoMember(1)] public ProtoExceptionModel Exception { get; set; }
        public bool IsSucceeded => Exception == null;

        public void EnsureSuccess()
        {
            if (!IsSucceeded)
            {
                throw Exception;
            }
        }

        public static implicit operator ProtoExceptionModel(Response response)
        {
            return response.Exception;
        }

        public static implicit operator Response(ProtoExceptionModel exception)
        {
            return new Response()
            {
                Exception = exception
            };
        }

        public static implicit operator ActionResult(Response response)
        {
            if (response.IsSucceeded)
            {
                return new OkResult();
            }

            return response.Exception.ToActionResult();
        }
    }
}