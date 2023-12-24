using System;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Application.CQRS;
using ApiGateway.Client.Application.CQRS.Queries;
using ApiGateway.Client.Domain;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Infrastructure
{
    public class PlayerModelFactory
    {
        private readonly Mediator _mediator;
        private readonly ApiGatewayClient _client;

        public PlayerModelFactory(Mediator mediator, ApiGatewayClient client)
        {
            _client = client;
            _mediator = mediator;
        }

        public async Task<PlayerModel> Create(AuthorizationToken token)
        {
            var user = GetUserByToken(token);
            if (!user.Roles.Has(Roles.Player))
                throw new InvalidOperationException("Role player mismatch");

            var saved = await _client.SaveVerdictHistory(token);
            if (!saved.IsSuccess)
            {
                throw new ErrorCodeException(ErrorCode.Exception);
            }

            var tasks = await CreateTaskCollection(token);
            return new PlayerModel(user, tasks);
        }

        private UserModel GetUserByToken(AuthorizationToken token)
        {
            switch (token)
            {
                case JwtAccessToken jwtAccessToken:
                    return new UserModel()
                    {
                        Email = jwtAccessToken.email,
                        FirstName = jwtAccessToken.firstname,
                        Roles = new Roles(jwtAccessToken.roles)
                    };
                case VkAccessToken vkAccessToken:
                    return new UserModel()
                    {
                        Email = vkAccessToken.JwtAccessToken.email,
                        FirstName = vkAccessToken.JwtAccessToken.firstname,
                        Roles = new Roles(vkAccessToken.JwtAccessToken.roles)
                    };
            }

            return null;
        }

        private async Task<TaskCollection> CreateTaskCollection(AuthorizationToken token)
        {
            var query = new FetchTaskCollection()
            {
                Token = token
            };

            var tasks = await _mediator.Send(query);
            return tasks;
        }
    }
}