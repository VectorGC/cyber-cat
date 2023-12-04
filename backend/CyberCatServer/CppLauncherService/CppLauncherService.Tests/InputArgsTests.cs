using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared.Server.Data;
using Shared.Server.Infrastructure;
using Shared.Server.Services;
using Shared.Tests;

namespace CppLauncherService.Tests;

[TestFixture]
public class InputArgsTests
{
    private WebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Test]
    public async Task CanOutputInputArgsAsInt()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { int arg; scanf(\"%d\", &arg); printf(\"%d\", arg); }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"10"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("10", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsDouble()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { double arg; scanf(\"%lf\", &arg); printf(\"%lf\", arg); }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"1.1111115"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        // This is normal, rounding occurs.
        Assert.AreEqual("1.111112", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsDoubleWithSmallDigit()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { double arg; scanf(\"%lf\", &arg); printf(\"%lf\", arg); }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"1.1"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("1.100000", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsString()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { char arg[25]; scanf(\"%s\", arg); printf(\"%s\", arg); }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"Hello"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("Hello", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputTwoInputArgsAsString()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { char arg1[25]; char arg2[25]; scanf(\"%s%s\", arg1, arg2); printf(\"%s%s\", arg1, arg2); }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"Hello Cat"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("HelloCat", output.StandardOutput);
    }
}