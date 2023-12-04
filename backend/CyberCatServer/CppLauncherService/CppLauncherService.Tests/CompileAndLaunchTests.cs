using CppLauncherService.Application;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Client;
using Shared.Server.Data;
using Shared.Server.Services;
using Shared.Tests;

namespace CppLauncherService.Tests;

// Checking if the code can compile and run on our service.
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

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(new LaunchCodeArgs(sourceCode));

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("Hello cat!", output.StandardOutput);
    }

    [Test]
    public async Task CompileError_WhenPassNonCompiledCode()
    {
        const string sourceCode = "#include <stdio.h> \nint main()";
        const string expectedErrorRegex = "Exit Code 1:.*:2:11: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(new LaunchCodeArgs(sourceCode));

        Assert.That(output.StandardError, Does.Match(expectedErrorRegex));
        Assert.IsNull(output.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassInfinityLoopCode()
    {
        const string sourceCode = "int main() { while(true){} }";
        var appSettings = _factory.Services.GetRequiredService<IOptions<CppLauncherAppSettings>>();
        var expectedError = $"Exit Code -1: The process took more than {appSettings.Value.ProcessTimeoutSec} seconds";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(new LaunchCodeArgs(sourceCode));

        Assert.AreEqual(output.StandardError, expectedError);
        Assert.IsNull(output.StandardOutput);
    }

    [Test]
    public async Task LaunchError_WhenPassSegmentationFaultCode()
    {
        const string sourceCode = "#include <stdio.h> \nint main() { int *p = NULL; *p = 1; }";
        const string expectedError = "Exit Code 11: (SIGSEGV signal) Segmentation fault";

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(new LaunchCodeArgs(sourceCode));

        Assert.AreEqual(expectedError, output.StandardError);
        Assert.IsNull(output.StandardOutput);
    }
}