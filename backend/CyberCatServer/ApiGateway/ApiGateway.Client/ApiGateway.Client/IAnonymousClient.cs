using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public interface IAnonymousClient
    {
        IAuthorizationService Authorization { get; }
        Task<string> GetStringTestAsync(string uri);
    }
}