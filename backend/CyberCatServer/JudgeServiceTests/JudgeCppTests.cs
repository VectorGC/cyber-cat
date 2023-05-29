using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Services;

namespace JudgeServiceTests;

public class JudgeCppTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    public async Task SuccessVerdictHelloCatTask_WhenPassValidCode()
    {
        
    }
    
    public async Task WrongVerdictHelloCatTask_WhenPassIncorrectCode()
    {
        
    }
    
    public async Task WrongVerdictHelloCatTask_WhenPassNonCompileCode()
    {
        
    }

    [Test]
    public async Task CompileAndLaunch_WhenPassValidCode()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(sourceCode);

        Assert.IsNull(response.StandardError);
        Assert.AreEqual("Hello cat!", response.StandardOutput);
    }

    [Test]
    public async Task CompileError_WhenPassNonCompiledCode()
    {
        const string sourceCode = "#include <stdio.h> \nint main()";
        const string expectedErrorRegex = "Exit Code 1:.*:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(sourceCode);

        Assert.That(response.StandardError, Does.Match(expectedErrorRegex));
        Assert.IsNull(response.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassInfinityLoopCode()
    {
        const string sourceCode = "int main() { while(true){} }";
        const string expectedError = "Exit Code -1: The process took more than 5 seconds";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(sourceCode);

        Assert.AreEqual(expectedError, response.StandardError);
        Assert.IsNull(response.StandardOutput);
    }
}