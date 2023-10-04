using Microsoft.AspNetCore.Mvc;
using ProtoBuf;
using Shared.Server.Exceptions;

namespace Shared.Server.ProtoHelpers;

[ProtoContract]
public class Response<TValue> where TValue : class
{
    [ProtoMember(1)] public TValue Value { get; set; }
    [ProtoMember(2)] public ProtoExceptionModel Exception { get; set; }

    public bool HasValue => Value != null;
    public bool IsSucceeded => Exception == null;

    public void EnsureSuccess()
    {
        if (!IsSucceeded)
        {
            throw Exception;
        }
    }

    public static implicit operator TValue(Response<TValue> response)
    {
        return response.Value;
    }

    public static implicit operator Response<TValue>(TValue value)
    {
        return new Response<TValue>()
        {
            Value = value
        };
    }

    public static implicit operator ProtoExceptionModel(Response<TValue> response)
    {
        return response.Exception;
    }

    public static implicit operator Response<TValue>(ProtoExceptionModel exception)
    {
        return new Response<TValue>()
        {
            Exception = exception
        };
    }

    public static implicit operator ActionResult(Response<TValue> response)
    {
        if (response.IsSucceeded)
        {
            return new OkResult();
        }

        return response.Exception.ToActionResult();
    }

    public static implicit operator ActionResult<TValue>(Response<TValue> response)
    {
        if (response.IsSucceeded)
        {
            return new OkObjectResult(response.Value);
        }

        return response.Exception.ToActionResult();
    }
}