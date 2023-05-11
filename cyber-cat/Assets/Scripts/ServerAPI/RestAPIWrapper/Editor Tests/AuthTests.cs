using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace RestAPIWrapper.EditorTests
{
    [TestFixture]
    public class AuthTests
    {
        [Test]
        [RequiresPlayMode]
        public async Task WhenAuth_AndCorrectEmailAndPassword_ThenTokenIsNotEmpty()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";

            //Act. Совершение действия.
            var token = await RestAPI.Instance.GetAuthData(login, password);

            //Assert. Проверка результата.
            /*
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
            */
        }

        [Test]
        public async Task WhenAuth_AndCorrectEmailAndPasswordAndServerless_ThenTokenIsNotEmpty()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var serverless = new Serverless.RestAPIServerless();

            //Act. Совершение действия.
            var token = await serverless.GetAuthData(login, password);

            //Assert. Проверка результата.
            /*
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
            */
        }

        [Test]
        [RequiresPlayMode]
        public async Task WhenAuth_AndCorrectEmailAndPasswordAndServer_ThenTokenIsNotEmpty()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var server = new V1.RestAPIV1();

            //Act. Совершение действия.
            var token = await server.GetAuthData(login, password);

            //Assert. Проверка результата.
            /*
            Assert.IsNotEmpty(token.Token);
            Assert.IsNotEmpty(token.Name);
            Assert.IsEmpty(token.Error);
            */
        }
    }
}


