using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;

namespace ApiGateway.Client.Application.Middlewares
{
    public class AuthorizedGuardMiddleware : IMiddleware
    {
        private readonly PlayerContext _playerContext;

        public AuthorizedGuardMiddleware(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
        {
            if (context.Command is IAuthorizedOnly)
            {
                if (!_playerContext.IsLogined)
                    throw new ErrorCodeException(ErrorCode.NotLoggined);
            }

            return await next.Invoke(context);
        }
    }
}