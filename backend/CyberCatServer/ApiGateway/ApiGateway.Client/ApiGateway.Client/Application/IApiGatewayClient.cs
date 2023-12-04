using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application.API;
using ApiGateway.Client.Application.UseCases;
using ApiGateway.Client.Domain;

namespace ApiGateway.Client.Application
{
    public interface IApiGatewayClient : IDisposable
    {
        PlayerAPI Player { get; }
        Task<Result> RegisterPlayer(string email, string password, string userName);
        Task<Result<PlayerModel>> LoginPlayer(string email, string password);
    }
}