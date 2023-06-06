using ApiGateway.Client.Tests.Core.Tests;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.WebClient
{
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
        protected override string Host => TestEnvironment.Host;
        protected override IClient Client => TestEnvironment.Client;
        protected override IRestClient RestClient => TestEnvironment.RestClient;
    }

    [TestFixture]
    public class Tasks : TaskTests
    {
        protected override string Host => TestEnvironment.Host;
        protected override IClient Client => TestEnvironment.Client;
        protected override IRestClient RestClient => TestEnvironment.RestClient;
    }

    [TestFixture]
    public class VerifySolution : VerifySolutionTests
    {
        protected override string Host => TestEnvironment.Host;
        protected override IClient Client => TestEnvironment.Client;
        protected override IRestClient RestClient => TestEnvironment.RestClient;
    }
}