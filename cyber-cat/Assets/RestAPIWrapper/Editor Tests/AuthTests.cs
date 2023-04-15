using System.Threading.Tasks;
using Authentication;
using Newtonsoft.Json;
using NUnit.Framework;

namespace RestAPIWrapper.EditorTests
{
    [TestFixture]
    public class AuthTests
    {
        [Test]
        public async Task ShouldGetToken_WhenPassEmailAndPassword()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";

            //Act. Совершение действия.
            var tokenJsonObj = await RestAPI.Instance.GetAuthData(login, password);
            var token = JsonConvert.DeserializeObject<TokenSession>(tokenJsonObj);

            //Assert. Проверка результата.
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
        }

        [Test]
        public async Task ShouldGetToken_Serverless()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var serverless = new Serverless.RestAPIServerless();

            //Act. Совершение действия.
            var tokenJsonObj = await serverless.GetAuthData(login, password);
            var token = JsonConvert.DeserializeObject<TokenSession>(tokenJsonObj);

            //Assert. Проверка результата.
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
        }


    }
}


