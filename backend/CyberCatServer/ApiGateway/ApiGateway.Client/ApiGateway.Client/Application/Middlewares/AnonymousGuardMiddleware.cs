using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;

namespace ApiGateway.Client.Application.Middlewares
{
    public class AnonymousGuardMiddleware : IMiddleware
    {
        private readonly PlayerContext _playerContext;

        public AnonymousGuardMiddleware(PlayerContext playerContext)
        {
            _playerContext = playerContext;
        }

        public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
        {
            if (context.Command is IAnonymousOnly)
            {
                if (_playerContext.IsLogined)
                    throw new ErrorCodeException(ErrorCode.AlreadyLoggined);
            }

            return await next.Invoke(context);
        }
    }
}