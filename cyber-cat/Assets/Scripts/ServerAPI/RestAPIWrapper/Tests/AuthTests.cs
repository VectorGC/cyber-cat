using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace RestAPIWrapper.Tests
{
    [TestFixture]
    public class AuthTests
    {
        // https://gitlab.com/karim.kimsanbaev/cyber-cat/-/wikis/%D0%A2%D0%B5%D1%81%D1%82%D1%8B:-%D0%AE%D0%BD%D0%B8%D1%82-%D1%82%D0%B5%D1%81%D1%82%D1%8B-%D0%BD%D0%B0-%D0%BA%D0%BB%D0%B8%D0%B5%D0%BD%D1%82%D0%B5

        [Test]
        public async Task ShouldGetToken_WhenPassEmailAndPassword()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";

            //Act. Совершение действия.
            var tokenJsonObj = await RestAPI.Instance.GetAuthData(login, password);
            //var token = JsonConvert.DeserializeObject<TokenSession>(tokenJsonObj);

            //Assert. Проверка результата.
            /*
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
            */
        }
    }
}