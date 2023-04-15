using System.Threading.Tasks;
using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace RestAPIWrapper.EditorTests
{
    [TestFixture]
    public class AuthTests
    {
        [Test]
        public async Task WhenAuth_AndCorrectEmailAndPassword_ThenTokenIsNotEmpty()
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
        public async Task WhenAuth_AndCorrectEmailAndPasswordAndServerless_ThenTokenIsNotEmpty()
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

        [Test]
        [RequiresPlayMode]
        public async Task WhenAuth_AndCorrectEmailAndPasswordAndServer_ThenTokenIsNotEmpty()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var server = new Server.RestAPIServer();

            //Act. Совершение действия.
            var tokenJsonObj = await server.GetAuthData(login, password);
            var token = JsonConvert.DeserializeObject<TokenSession>(tokenJsonObj);

            //Assert. Проверка результата.
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
        }
    }
}


