using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc.Client;
using Shared.Server.Data;
using Shared.Server.Infrastructure;
using Shared.Server.Services;
using Shared.Tests;

namespace CppLauncherService.Tests;

[TestFixture]
// Проверяем, поддерживает ли сервис #include <iostream> и cin и cout.
public class IncludeIOStreamTests
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
        const string sourceCode = "#include <iostream>\nint main() { int arg; std::cin >> arg; std::cout << arg;}";
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
        const string sourceCode = "#include <iostream>\nint main() { double arg; std::cin>>arg; std::cout<<arg; }";
        var args = new LaunchCodeArgs(sourceCode, new[] {"1.1111115"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        // Это нормально, происходит округление.
        Assert.AreEqual("1.11111", output.StandardOutput);
    }

    [Test]
    public async Task CanOutputInputArgsAsDoubleWithSmallDigit()
    {
        const string sourceCode = "#include <iostream>\nint main() { double arg; std::cin>>arg; std::cout<<std::fixed<<arg; }";
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
        const string sourceCode = "#include <iostream>\nint main() { char arg[25]; std::cin >> arg; std::cout<< arg; }";
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
        const string sourceCode = "#include <iostream>\nint main() { char arg1[25]; char arg2[25]; std::cin >> arg1; std::cin >> arg2; std::cout<< arg1 << arg2;}";
        var args = new LaunchCodeArgs(sourceCode, new[] {"Hello\nCat"});

        using var channel = _factory.CreateGrpcChannel();
        var codeLauncherService = channel.CreateGrpcService<ICodeLauncherService>();

        var output = (OutputDto) await codeLauncherService.Launch(args);

        Assert.IsNull(output.StandardError);
        Assert.AreEqual("HelloCat", output.StandardOutput);
        Console.WriteLine(output.StandardOutput);
    }
}