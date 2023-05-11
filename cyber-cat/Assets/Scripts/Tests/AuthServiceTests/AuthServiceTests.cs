using System.Threading.Tasks;
using AuthService;
using NUnit.Framework;
using Services.AuthService;

namespace Tests.AuthServiceTests
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
    }
}