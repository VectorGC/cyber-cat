using NUnit.Framework;

namespace RestAPIWrapper.EditorTests
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void WhenRegisteringUser_AndCorrectLoginAndPasswordAndName_ThenTaskIsNotNull()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var name = "Karpik";

            //Act. Совершение действия.
            var uniTask = RestAPI.Instance.RegisterUser(login, password, name);

            //Assert. Проверка результата.
            Assert.IsNotNull(uniTask);
        }

        [Test]
        public void WhenRegisteringUser_AndCorrectLoginAndPasswordAndNameServer_ThenTaskIsNotNull()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var name = "Karpik";

            //Act. Совершение действия.
            var uniTask = new V1.RestAPIV1().RegisterUser(login, password, name);

            //Assert. Проверка результата.
            Assert.IsNotNull(uniTask);
        }

        [Test]
        public void WhenRegisteringUser_AndServerless_ThenTaskIsNotNull()
        {
            //Arrange. Подготовка данных
            var login = "Karpik";
            var password = "123";
            var name = "Karpik";

            //Act. Совершение действия.
            var uniTask = new Serverless.RestAPIServerless().RegisterUser(login, password, name);

            //Assert. Проверка результата.
            Assert.IsNotNull(uniTask);
        }
    }
}
