using ApiGateway.Client;
using ApiGateway.Client.Tests.Core.Tests;
using NUnit.Framework;
using ServerAPI;

public class TestEnvironment
{
    public const string Host = "http://localhost:5000";
    public static IClient Client = new Client(Host, new UnityRestClientAdapter());
    public static IRestClient RestClient = new UnityRestClientAdapter();
}

[TestFixture]
public class Authenticate : AuthenticateTests
{
    protected override string Host { get; } = TestEnvironment.Host;
    protected override IClient Client { get; } = TestEnvironment.Client;
    protected override IRestClient RestClient { get; } = TestEnvironment.RestClient;
}

[TestFixture]
public class Solutions : SolutionTests
{
    protected override string Host { get; } = TestEnvironment.Host;
    protected override IClient Client { get; } = TestEnvironment.Client;
    protected override IRestClient RestClient { get; } = TestEnvironment.RestClient;
}

[TestFixture]
public class Tasks : TaskTests
{
    protected override string Host { get; } = TestEnvironment.Host;
    protected override IClient Client { get; } = TestEnvironment.Client;
    protected override IRestClient RestClient { get; } = TestEnvironment.RestClient;
}

[TestFixture]
public class VerifySolution : VerifySolutionTests
{
    protected override string Host { get; } = TestEnvironment.Host;
    protected override IClient Client { get; } = TestEnvironment.Client;
    protected override IRestClient RestClient { get; } = TestEnvironment.RestClient;
}