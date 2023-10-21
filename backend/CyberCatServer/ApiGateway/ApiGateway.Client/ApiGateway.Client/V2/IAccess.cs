using System;

namespace ApiGateway.Client.V2
{
    public interface IAccess : IDisposable
    {
        bool IsAvailable { get; }
    }
}