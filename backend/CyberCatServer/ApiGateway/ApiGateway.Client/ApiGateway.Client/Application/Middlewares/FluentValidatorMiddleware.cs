using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Application.CQRS;
using FluentValidation;

namespace ApiGateway.Client.Application.Middlewares
{
    public class FluentValidatorMiddleware : IMiddleware
    {
        private readonly TinyIoCContainer _container;

        public FluentValidatorMiddleware(TinyIoCContainer container)
        {
            _container = container;
        }

        public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(context.Command.GetType());
            if (_container.TryResolve(validatorType, out var validator))
            {
                var validatorTyped = (IValidator) validator;
                var result = await validatorTyped.ValidateAsync(context.Command);
                if (!result.IsValid)
                {
                    var error = result.Errors.First();
                    if (Enum.TryParse<ErrorCode>(error.ErrorCode, out var errorCode))
                        throw new ErrorCodeException(errorCode);

                    throw new ErrorCodeException(ErrorCode.Unknown, error.ErrorMessage);
                }
            }

            return await next.InvokeAsync(context);
        }
    }

    public static class FluentValidatorMiddlewareExtensions
    {
        public static void RegisterCommand<TCommand, TCommandHandler, TValidator>(this TinyIoCContainer container)
            where TCommand : ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
            where TValidator : class, IValidator<TCommand>
        {
            container.Register<ICommandHandler<TCommand>, TCommandHandler>().AsSingleton();
            container.Register<IValidator<TCommand>, TValidator>().AsSingleton();
        }
    }
}