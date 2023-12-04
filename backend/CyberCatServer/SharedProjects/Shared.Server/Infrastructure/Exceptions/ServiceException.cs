using System.Net;
using Grpc.Core;

namespace Shared.Server.Infrastructure.Exceptions;

public sealed class ServiceException : RpcException
{
    public ServiceException(string message, HttpStatusCode statusCode) : base(new Status(StatusCode.Internal, message), new Metadata()
    {
        new Metadata.Entry(nameof(HttpStatusCode), statusCode.ToString())
    })
    {
    }
}