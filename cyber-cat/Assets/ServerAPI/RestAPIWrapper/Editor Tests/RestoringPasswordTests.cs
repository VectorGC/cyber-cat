using System.Threading.Tasks;
using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace RestAPIWrapper.EditorTests
{
    public class RestoringPasswordTests
    {
        [Test]
        public void WhenRestoringPassword_AndServerless_ThenTaskIsNotNull()
        {
            //Arrange
            var login = "Karpik";
            var pass = "123";

            //Act
            var uniTask = new Serverless.RestAPIServerless().RestorePassword(login, pass);

            //Assert
            Assert.IsNotNull(uniTask);
        }

        [Test]
        public void WhenRestoringPassword_AndLoginAndPAsswordAreCorrect_ThenTaskIsNotNull()
        {
            //Arrange
            var login = "Karpik";
            var pass = "123";

            //Act
            var uniTask = RestAPI.Instance.RestorePassword(login, pass);

            //Assert
            Assert.IsNotNull(uniTask);
        }

        [Test]
        public void WhenRestoringPassword_AndLoginAndPAsswordAreCorrectAndServer_ThenTaskIsNotNull()
        {
            //Arrange
            var login = "Karpik";
            var pass = "123";

            //Act
            var uniTask = new V1.RestAPIV1().RestorePassword(login, pass);

            //Assert
            Assert.IsNotNull(uniTask);
        }
    }
}
