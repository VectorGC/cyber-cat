using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;

namespace ApiGateway.Client.Application.Middlewares
{
    public class CatchExceptionMiddleware : IMiddleware
    {
        public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
        {
            try
            {
                return await next.InvokeAsync(context);
            }
            catch (WebException e)
            {
                return e;
            }
            catch (ErrorCodeException e)
            {
                return e;
            }
        }
    }
}