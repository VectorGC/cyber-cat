using ApiGateway.Client.Services.Authorization;

namespace ApiGateway.Client
{
    public interface IAnonymousClient
    {
        IAuthorizationService Authorization { get; }
    }
}