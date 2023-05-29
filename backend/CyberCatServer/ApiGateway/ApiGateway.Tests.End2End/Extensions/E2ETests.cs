namespace ApiGateway.Tests.End2End.Extensions;

[TestFixture]
public abstract class E2ETests
{
    protected TestUserHttpClient Client;

    [SetUp]
    public async Task SetUp()
    {
        Client = await TestUserHttpClient.Create();
    }

    [TearDown]
    public async Task TearDown()
    {
        await Client.DisposeAsync();
    }
}