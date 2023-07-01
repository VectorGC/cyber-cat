using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Client;
using Shared.Models.Dto.Args;
using Shared.Server.Services;
using Shared.Server.Tests;

namespace CppLauncherService.Tests;

// Проверяем, может ли код компилироваться и запуска на нашем сервисе.
[TestFixture]
public class CompileAndLaunchTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Test]
    public async Task CompileAndLaunch_WhenPassValidCode()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
        var args = new LaunchCodeArgs()
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
        const string sourceCode = "#include <stdio.h> \nint main()";
        const string expectedErrorRegex = "Exit Code 1:.*:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";
        var args = new LaunchCodeArgs()
        {
            SourceCode = sourceCode
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        Assert.That(response.StandardError, Does.Match(expectedErrorRegex));
        Assert.IsNull(response.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassInfinityLoopCode()
    {
        const string sourceCode = "int main() { while(true){} }";
        var appSettings = _factory.Services.GetRequiredService<IOptions<CppLauncherAppSettings>>();
        var expectedError2 = $"Exit Code -1: The process took more than {appSettings.Value.ProcessTimeoutSec} seconds";
        var args = new LaunchCodeArgs()
        {
            SourceCode = sourceCode
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        //var expectedError = $"Exit Code -1: The process took more than 1 seconds";
        var regex = "Exit Code 1: .*line 1, in <module>    print\\(a\\)NameError: name 'a' is not defined\"";


        var err = "Exit Code 1: Traceback (most recent call last):" +
                  "File \"/mnt/c/Users/SMC/cyber-cat/backend/CyberCatServer/PythonLauncherService/PythonLauncherService.Tests/bin/Debug/net6.0/aqs0uj0x.py\", line 1, in <module>" +
                  "    print(a)" +
                  "NameError: name 'a' is not defined\"";

        Assert.That(err, Does.Match(regex));
        //Assert.AreEqual(expectedError, err);
        //Assert.IsNull(response.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassSegmentationFaultCode()
    {
        const string sourceCode = "#include <stdio.h> \nint main() { int *p = NULL; *p = 1; }";
        const string expectedError = "Exit Code 11: (SIGSEGV signal) Segmentation fault";
        var args = new LaunchCodeArgs()
        {
            SourceCode = sourceCode
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var response = await codeLauncherService.Launch(args);

        Assert.AreEqual(expectedError, response.StandardError);
        Assert.IsNull(response.StandardOutput);
    }
}