using System;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Application.API;
using ApiGateway.Client.V3.Domain;
using ApiGateway.Client.V3.Infrastructure;

namespace ApiGateway.Client.V3.Application
{
    public interface IApiGatewayClient : IDisposable
    {
        PlayerAPI Player { get; }
        Task<Result> RegisterPlayer(string email, string password, string userName);
        Task<Result<PlayerModel>> LoginPlayer(string email, string password);
    }
}