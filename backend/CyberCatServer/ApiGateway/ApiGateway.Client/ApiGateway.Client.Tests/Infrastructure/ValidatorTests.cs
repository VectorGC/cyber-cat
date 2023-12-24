using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.Application;
using ApiGateway.Client.Application.CQRS;
using ApiGateway.Client.Application.Middlewares;
using FluentValidation;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Infrastructure
{
    [TestFixture]
    public class ValidatorTests
    {
        public class PrintCommandValidator : AbstractValidator<MediatorTests.PrintCommand>
        {
            public PrintCommandValidator()
            {
                RuleFor(x => x.Message).NotEmpty().WithMessage("Сообщение не может быть пустым");
            }
        }

        public class PrintCommandValidatorWithErrorCode : AbstractValidator<MediatorTests.PrintCommand>
        {
            public PrintCommandValidatorWithErrorCode()
            {
                RuleFor(x => x.Message).NotEmpty().WithErrorCode(ErrorCode.Unknown.ToString()).WithMessage("Сообщение не может быть пустым");
            }
        }

        [Test]
        public async Task SuccessValidate_WhenValidModel()
        {
            var command = new MediatorTests.PrintCommand()
            {
                Message = "some"
            };

            var validator = new PrintCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.IsTrue(result.IsValid);
            CollectionAssert.IsEmpty(result.Errors);
        }

        [Test]
        public async Task FailureValidate_WhenInvalidModel()
        {
            var command = new MediatorTests.PrintCommand();

            var validator = new PrintCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Сообщение не может быть пустым", result.Errors.First().ErrorMessage);
        }

        [Test]
        public async Task SuccessValidate_WhenMediatorValidateMiddleware()
        {
            var container = new TinyIoCContainer();

            container.Register<Mediator>();
            container.RegisterCommand<MediatorTests.PrintCommand, MediatorTests.PrintCommandHandler, PrintCommandValidator>();
            container.UseMiddleware<FluentValidatorMiddleware>();

            var mediator = container.Resolve<Mediator>();
            await mediator.Send(new MediatorTests.PrintCommand()
            {
                Message = "Hello"
            });

            Assert.That(MediatorTests.PrintCommandHandler.Output, Is.EqualTo("Hello"));
        }

        [Test]
        public async Task FailureValidate_WhenMediatorValidateMiddleware()
        {
            var container = new TinyIoCContainer();

            container.Register<Mediator>();
            container.RegisterCommand<MediatorTests.PrintCommand, MediatorTests.PrintCommandHandler, PrintCommandValidator>();
            container.UseMiddleware<CatchExceptionMiddleware>();
            container.UseMiddleware<FluentValidatorMiddleware>();

            var mediator = container.Resolve<Mediator>();
            var response = await mediator.SendSafe(new MediatorTests.PrintCommand());
            var result = Result.FromObject(response);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Сообщение не может быть пустым", result.Error);
        }

        [Test]
        public async Task FailureValidate_WhenMediatorValidateMiddleware_WithErrorCode()
        {
            var container = new TinyIoCContainer();

            container.Register<Mediator>();
            container.RegisterCommand<MediatorTests.PrintCommand, MediatorTests.PrintCommandHandler, PrintCommandValidatorWithErrorCode>();
            container.UseMiddleware<CatchExceptionMiddleware>();
            container.UseMiddleware<FluentValidatorMiddleware>();

            var mediator = container.Resolve<Mediator>();
            var response = await mediator.SendSafe(new MediatorTests.PrintCommand());
            var result = Result.FromObject(response);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Неизвестная ошибка. Обратитесь к администратору", result.Error);
        }
    }
}