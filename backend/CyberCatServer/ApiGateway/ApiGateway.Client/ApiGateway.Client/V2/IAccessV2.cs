using System;

namespace ApiGateway.Client.V2
{
    public interface IAccessV2 : IDisposable
    {
        bool IsAvailable { get; }
    }
}