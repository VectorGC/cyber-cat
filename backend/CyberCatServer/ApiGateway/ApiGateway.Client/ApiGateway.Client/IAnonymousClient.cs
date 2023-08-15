using ApiGateway.Client.Services;

namespace ApiGateway.Client
{
    public interface IAnonymousClient
    {
        IAuthorizationService Authorization { get; }
    }
}