using System.Threading.Tasks;
using AuthService;
using NUnit.Framework;
using RestAPI;
using Services.AuthService;
using Tests.InternalMockModels;

namespace Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private readonly IAuthService _service = new AuthServiceRestProxy(RestAPIFacade.Create());

        [Test]
        public async Task ShouldTokenIsNotEmpty_WhenCorrectCredentials([Values("test@test.com")] string login, [Values("test")] string password)
        {
            var token = await _service.Authenticate(login, password);

            Assert.IsNotEmpty(token.Value);
        }

        [Test]
        public async Task ShouldAuthorizeAsPlayer_WhenCorrectToken([Values("correct_token")] string token)
        {
            var player = await _service.AuthorizeAsPlayer(new MockToken(token));

            Assert.IsNotEmpty(player.Name);
            Assert.AreEqual(token, player.Token.Value);
        }
    }
}