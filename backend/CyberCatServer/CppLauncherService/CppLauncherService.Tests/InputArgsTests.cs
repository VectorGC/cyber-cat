using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared;
using Shared.Dto.Args;
using Shared.Services;

namespace CppLauncherService.Tests;

// Проверяем корректно ли работает сервис с выходным потоком.
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
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = "10"
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var output = await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("10", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsDouble()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { double arg; scanf(\"%lf\", &arg); printf(\"%lf\", arg); }";
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = "1.1111115"
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var output = await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        // Это нормально, происходит округление.
        Assert.AreEqual("1.111112", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsDoubleWithSmallDigit()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { double arg; scanf(\"%lf\", &arg); printf(\"%lf\", arg); }";
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = "1.1"
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var output = await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("1.100000", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsString()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { char arg[25]; scanf(\"%s\", arg); printf(\"%s\", arg); }";
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = "Hello"
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var output = await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("Hello", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputTwoInputArgsAsString()
    {
        const string sourceCode = "#include <stdio.h>\nint main() { char arg1[25]; char arg2[25]; scanf(\"%s%s\", arg1, arg2); printf(\"%s%s\", arg1, arg2); }";
        var args = new LaunchCodeArgs
        {
            SourceCode = sourceCode,
            Input = "Hello Cat"
        };

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherGrpcService>();

        var output = await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("HelloCat", output.StandardOutput);
    }
}