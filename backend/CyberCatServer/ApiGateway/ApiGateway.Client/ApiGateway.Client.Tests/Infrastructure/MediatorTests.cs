using System.Threading.Tasks;
using ApiGateway.Client.Application;
using NUnit.Framework;
using Shared.Models;

namespace ApiGateway.Client.Tests.Infrastructure
{
    [TestFixture]
    public class MediatorTests
    {
        public class PingCommand : IQuery<string>
        {
            public string Message { get; set; }
        }

        public class PingCommandHandler : IQueryHandler<PingCommand, string>
        {
            public Task<string> Handle(PingCommand command)
            {
                return Task.FromResult(command.Message);
            }
        }

        public class PrintCommand : ICommand
        {
            public string Message { get; set; }
        }

        public class PrintCommandHandler : ICommandHandler<PrintCommand>
        {
            public static string Output;

            public Task Handle(PrintCommand command)
            {
                Output = command.Message;
                return Task.CompletedTask;
            }
        }

        public class AddSymbolToResponseMiddleware : IMiddleware
        {
            public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
            {
                var response = await next.InvokeAsync(context);
                if (response is string str)
                {
                    str += "_middleware";
                    response = str;
                }

                return response;
            }
        }

        public class AddSymbolToPrintCommandMiddleware : IMiddleware
        {
            public async Task<object> InvokeAsync(Context context, IMiddlewareNode next)
            {
                if (context.Command is PrintCommand printCommand)
                {
                    printCommand.Message += "_middleware";
                }

                var response = await next.InvokeAsync(context);
                return response;
            }
        }

        [Test]
        public async Task SendCommandWithResponse()
        {
            var container = new TinyIoCContainer();

            container.Register<Mediator>();
            container.RegisterCommand<PrintCommand, PrintCommandHandler>();

            var mediator = container.Resolve<Mediator>();
            await mediator.Send(new PrintCommand()
            {
                Message = "Hello"
            });

            Assert.That(PrintCommandHandler.Output, Is.EqualTo("Hello"));
        }

        [Test]
        public async Task SendCommandWithoutResponse()
        {
            var container = new TinyIoCContainer();

            container.Register<Mediator>();
            container.RegisterQuery<PingCommand, PingCommandHandler, string>();

            var mediator = container.Resolve<Mediator>();
            var response = await mediator.Send(new PingCommand()
            {
                Message = "Pong"
            });

            Assert.That(response, Is.EqualTo("Pong"));
        }

        [Test]
        public async Task MiddlewaresWithResponse()
        {
            var container = new TinyIoCContainer();
            container.Register<Mediator>();
            container.RegisterQuery<PingCommand, PingCommandHandler, string>();
            container.UseMiddleware<AddSymbolToResponseMiddleware>();

            var mediator = container.Resolve<Mediator>();
            var response = await mediator.Send(new PingCommand()
            {
                Message = "Pong"
            });

            Assert.That(response, Is.EqualTo("Pong_middleware"));
        }

        [Test]
        public async Task MiddlewaresWithoutResponse()
        {
            var container = new TinyIoCContainer();
            container.Register<Mediator>();
            container.RegisterCommand<PrintCommand, PrintCommandHandler>();
            container.UseMiddleware<AddSymbolToPrintCommandMiddleware>();

            var mediator = container.Resolve<Mediator>();
            await mediator.Send(new PrintCommand()
            {
                Message = "Hello"
            });

            Assert.That(PrintCommandHandler.Output, Is.EqualTo("Hello_middleware"));
        }
    }
}