using CompilerServiceAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Dto;
using Shared.Services;

namespace CppLauncherServiceTests;

[TestFixture]
public class CppLauncherServiceTests
{
    private WebApplicationFactory<Program> _factory;
    private const string UserPassword = "123";

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>(); //.AddScoped<Program, IAuthUserRepository>(_mockAuthUserRepository);

        /*
        using var scope = _factory.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IAuthUserRepository>();
        userRepository.Add(_user, UserPassword);
        */
    }

    [Test]
    public async Task CompileAndLaunch_WhenPassValidCode()
    {
        var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
        var args = new SolutionCodeArgs
        {
            SourceCode = sourceCode
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        Assert.IsNull(response.StandardError);
        Assert.AreEqual("Hello cat!", response.StandardOutput);
    }

    [Test]
    public async Task CompileError_WhenPassNonCompiledCode()
    {
        var sourceCode = "#include <stdio.h> \nint main()";
        var args = new SolutionCodeArgs
        {
            SourceCode = sourceCode
        };
        var expectedError = "Exit Code 1: code.cpp:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        Assert.AreEqual(expectedError, response.StandardError);
        Assert.IsNull(response.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassInfinityLoopCode()
    {
        var sourceCode = "int main() { while(true){} }";
        var args = new SolutionCodeArgs
        {
            SourceCode = sourceCode
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        Assert.AreEqual("Exit Code -1: The process took more than 5 seconds", response.StandardError);
        Assert.IsNull(response.StandardOutput);
    }

    // TODO: Тест проверяющий асинхронную работу 3 - 10 запусков разных решений
}