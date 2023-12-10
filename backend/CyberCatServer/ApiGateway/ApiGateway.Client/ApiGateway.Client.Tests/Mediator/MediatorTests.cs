using System.Threading.Tasks;
using ApiGateway.Client.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Mediator
{
    [TestFixture]
    public class MediatorTests
    {
        public class PingCommand : ICommand<string>
        {
            public string Message { get; set; }
        }

        public class PingCommandHandler : ICommandHandler<PingCommand, string>
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
                var response = await next.Invoke(context);
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

                var response = await next.Invoke(context);
                return response;
            }
        }

        [Test]
        public async Task SendCommandWithResponse()
        {
            var container = new TinyIoCContainer();

            container.Register<Application.Mediator>();
            container.RegisterCommand<PrintCommand, PrintCommandHandler>();

            var mediator = container.Resolve<Application.Mediator>();
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

            container.Register<Application.Mediator>();
            container.RegisterCommand<PingCommand, string, PingCommandHandler>();

            var mediator = container.Resolve<Application.Mediator>();
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
            container.Register<Application.Mediator>();
            container.RegisterCommand<PingCommand, string, PingCommandHandler>();
            container.UseMiddleware<AddSymbolToResponseMiddleware>();

            var mediator = container.Resolve<Application.Mediator>();
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
            container.Register<Application.Mediator>();
            container.RegisterCommand<PrintCommand, PrintCommandHandler>();
            container.UseMiddleware<AddSymbolToPrintCommandMiddleware>();

            var mediator = container.Resolve<Application.Mediator>();
            await mediator.Send(new PrintCommand()
            {
                Message = "Hello"
            });

            Assert.That(PrintCommandHandler.Output, Is.EqualTo("Hello_middleware"));
        }
    }
}