using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Infrastructure;
using ApiGateway.Client.Infrastructure.WebClient;
using FluentValidation;
using Shared.Models.Infrastructure;
using Shared.Models.Infrastructure.Authorization;

namespace ApiGateway.Client.Application.CQRS.Commands
{
    public class LoginPlayer : ICommand, IAnonymousOnly
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginPlayerValidator : AbstractValidator<LoginPlayer>
    {
        public LoginPlayerValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithErrorCode(ErrorCode.EmailEmpty.ToString());
            RuleFor(x => x.Password).NotEmpty().WithErrorCode(ErrorCode.PasswordEmpty.ToString());
        }
    }

    public class LoginPlayerHandler : ICommandHandler<LoginPlayer>
    {
        private readonly WebClientFactory _webClientFactory;
        private readonly PlayerContext _playerContext;
        private readonly PlayerModelFactory _playerModelFactory;

        public LoginPlayerHandler(WebClientFactory webClientFactory, PlayerContext playerContext, PlayerModelFactory playerModelFactory)
        {
            _playerModelFactory = playerModelFactory;
            _playerContext = playerContext;
            _webClientFactory = webClientFactory;
        }

        public async Task Handle(LoginPlayer command)
        {
            var form = new Dictionary<string, string>
            {
                ["username"] = command.Email,
                ["password"] = command.Password
            };

            using (var client = _webClientFactory.Create())
            {
                var accessToken = await client.PostFastJsonAsync<JwtAccessToken>(WebApi.Login, form);
                var player = await _playerModelFactory.Create(accessToken);

                _playerContext.SetContext(player, accessToken);
            }
        }
        
        public async Task Handle(object command)
        {
            await Handle(command as LoginPlayer);
        }
    }
}