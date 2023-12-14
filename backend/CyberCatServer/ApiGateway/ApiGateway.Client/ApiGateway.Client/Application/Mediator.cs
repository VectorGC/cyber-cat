using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiGateway.Client.Application
{
    public class Mediator
    {
        private readonly TinyIoCContainer _container;

        public Mediator(TinyIoCContainer container)
        {
            _container = container;
            _container.Register<MiddlewareChain>();
        }

        public async Task Send(ICommand command)
        {
            await SendSafe(command);
        }

        public async Task<object> SendSafe(ICommand command)
        {
            var chain = _container.Resolve<MiddlewareChain>();
            var response = await chain.Execute(new Context()
            {
                Command = command
            });

            return response;
        }

        public async Task<TResult> Send<TResult>(IQuery<TResult> command)
        {
            var response = await SendSafe(command);
            return (TResult) response;
        }

        public async Task<object> SendSafe<TResult>(IQuery<TResult> command)
        {
            var chain = _container.Resolve<MiddlewareChain>();
            var response = await chain.Execute(new Context()
            {
                Command = command,
                ResultType = typeof(TResult)
            });

            return response;
        }
    }

    #region | CQRS |

    public interface ICommand
    {
    }

    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }

    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<in TCommand, TResult>
    {
        Task<TResult> Handle(TCommand command);
    }

    internal static class HandleCommand
    {
        public static async Task<object> Handle(object command, Type resultType, TinyIoCContainer container)
        {
            var handlerType = resultType != null
                ? typeof(IQueryHandler<,>).MakeGenericType(command.GetType(), resultType)
                : typeof(ICommandHandler<>).MakeGenericType(command.GetType());

            var handler = (dynamic) container.Resolve(handlerType);

            if (resultType != null)
            {
                var response = await handler.Handle((dynamic) command);
                return response;
            }
            else
            {
                await handler.Handle((dynamic) command);
                return null;
            }
        }
    }

    public static partial class MediatorExtensions
    {
        public static void RegisterCommand<TCommand, TCommandHandler>(this TinyIoCContainer container)
            where TCommand : ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
        {
            container.Register<ICommandHandler<TCommand>, TCommandHandler>().AsSingleton();
        }

        public static void RegisterQuery<TCommand, TCommandHandler, TResult>(this TinyIoCContainer container)
            where TCommand : IQuery<TResult>
            where TCommandHandler : class, IQueryHandler<TCommand, TResult>
        {
            container.Register<IQueryHandler<TCommand, TResult>, TCommandHandler>().AsSingleton();
        }
    }

    #endregion

    #region | Middlewares |

    public interface IMiddleware
    {
        Task<object> InvokeAsync(Context context, IMiddlewareNode next);
    }

    public class Context
    {
        public object Command;
        public Type ResultType;
    }

    public interface IMiddlewareNode
    {
        void SetNext(IMiddlewareNode next);
        Task<object> InvokeAsync(Context context);
    }

    public class MiddlewareNode : IMiddlewareNode
    {
        private readonly IMiddleware _middleware;
        private IMiddlewareNode _next;

        public MiddlewareNode(IMiddleware middleware)
        {
            _middleware = middleware;
        }

        public void SetNext(IMiddlewareNode next)
        {
            _next = next;
        }

        public async Task<object> InvokeAsync(Context context)
        {
            return await _middleware.InvokeAsync(context, _next);
        }
    }

    internal class MiddlewareChain
    {
        private readonly LinkedList<IMiddlewareNode> _middlewares = new LinkedList<IMiddlewareNode>();

        public MiddlewareChain(TinyIoCContainer container)
        {
            container.UseMiddleware<CommandHandlerMiddleware>();

            var middlewares = container.ResolveAll<IMiddleware>();
            foreach (var middleware in middlewares)
            {
                Use(middleware);
            }
        }

        private void Use(IMiddleware middleware)
        {
            var node = new MiddlewareNode(middleware);

            if (_middlewares.Count > 0)
            {
                var last = _middlewares.Last.Value;
                last?.SetNext(node);
            }

            _middlewares.AddLast(node);
        }

        public async Task<object> Execute(Context context)
        {
            var result = await _middlewares.First.Value.InvokeAsync(context);
            return result;
        }
    }

    internal class CommandHandlerMiddleware : IMiddleware
    {
        private readonly TinyIoCContainer _container;

        public CommandHandlerMiddleware(TinyIoCContainer container)
        {
            _container = container;
        }

        public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
        {
            return await HandleCommand.Handle(context.Command, context.ResultType, _container);
        }
    }

    public static partial class MediatorExtensions
    {
        public static void UseMiddleware<TMiddleware>(this TinyIoCContainer container)
            where TMiddleware : class, IMiddleware
        {
            container.Register<IMiddleware, TMiddleware>(typeof(TMiddleware).FullName).AsSingleton();
        }
    }

    #endregion
}