using NUnit.Framework;

namespace AuthService.Tests
{
    public class AuthServiceTests
    {
        private IAuthService _service;

        [SetUp]
        public void Setup()
        {
            _service = new AuthService();
        }

        [Test]
        public void WhenAuth_AndCorrectData_ThenTokenIsNotEmpty()
        {
            //Arrange
            var login = "Karpik";
            var password = "123";

            //Act
            var token = _service.Authenticate(login, password);

            //Assert
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
        }

        [Test]
        public void WhenAuth_AndWrongLogin_ThenErrorIsNotEmpty([Values("Karpik1337", "dfgdfgerg")] string login)
        {
            //Arrange
            var password = "123";

            //Act
            var token = _service.Authenticate(login, password);

            //Assert
            Assert.IsNotEmpty(token.Error);
        }

        [Test]
        public void WhenAuth_AndWrongPassword_ThenErrorIsNotEmpty([Values("123456", "qwerty", "sdfergrtj")] string password)
        {
            //Arrange
            var login = "Karpik";

            //Act
            var token = _service.Authenticate(login, password);

            //Assert
            Assert.IsNotEmpty(token.Error);
        }
    }
}