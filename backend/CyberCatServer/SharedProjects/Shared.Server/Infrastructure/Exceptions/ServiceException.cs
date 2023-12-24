using System;
using System.Net;
using Grpc.Core;

namespace Shared.Server.Infrastructure.Exceptions;

public sealed class ServiceException : RpcException
{
    public HttpStatusCode HttpStatusCode
    {
        get
        {
            var entry = Trailers.Get(nameof(HttpStatusCode));
            if (entry == null)
                return HttpStatusCode.BadRequest;

            return Enum.Parse<HttpStatusCode>(entry.Value);
        }
    }

    public new string Message => Status.Detail;

    public ServiceException(string message, HttpStatusCode statusCode) : base(new Status(StatusCode.Internal, message), new Metadata()
    {
        new Metadata.Entry(nameof(HttpStatusCode), statusCode.ToString())
    })
    {
    }
}

public static class ServiceExceptionExtension
{
    public static ServiceException ToServiceException(this RpcException rpcException)
    {
        var entry = rpcException.Trailers.Get(nameof(HttpStatusCode));
        if (entry == null)
            return null;

        var status = Enum.Parse<HttpStatusCode>(entry.Value);
        var message = rpcException.Status.Detail;

        return new ServiceException(message, status);
    }
}