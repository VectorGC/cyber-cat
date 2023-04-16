using System.Threading.Tasks;
using Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace RestAPIWrapper.EditorTests
{
    public class WebErrorlocalizeTests
    {
        [Test]
        public void WhenLocalize_AndErrorCodeIsZero_ThenNoErrors()
        {
            //Arrange
            var errorCode = "0";

            //Act
            var result = WebErrorLocalize.Localize(errorCode);

            //Assert
            Assert.IsTrue(result.Contains("Ошибок нет"));
        }

        [Test]
        public void WhenLocalize_AndErrorCodeIsNoError_ThenNoErrors()
        {
            //Arrange
            var errorCode = WebError.NoError;

            //Act
            var result = WebErrorLocalize.Localize(errorCode);

            //Assert
            Assert.IsTrue(result.Contains("Ошибок нет"));
        }

        [Test]
        public void WhenLocalize_AndErrorCodeIsNegative_ThenResultContainsInternalErrorCode()
        {
            //Arrange
            var errorCode = "-234234";

            //Act
            var result = WebErrorLocalize.Localize(errorCode);

            //Assert
            Assert.IsTrue(result.Contains($"Неизвестный код ошибки '{errorCode}'"));
        }

        [Test]
        public void WhenLocalize_AndErrorCodeIsNegative_ThenResultContainsInternalErrorCode_2()
        {
            //Arrange
            var errorCode = -234234;

            //Act
            var result = new WebErrorLocalize()[errorCode];

            //Assert
            Assert.IsTrue(result.Contains($"Неизвестный код ошибки '{errorCode}'"));
        }

        [Test]
        public void WhenLocalize_AndErrorCodedoesNotExist_ThenResultContainsInternalErrorCode()
        {
            //Arrange
            var errorCode = "10000000";

            //Act
            var result = WebErrorLocalize.Localize(errorCode);

            //Assert
            Assert.IsTrue(result.Contains($"Неизвестный код ошибки '{errorCode}'"));
        }

        [Test]
        public void WhenLocalize_AndErrorCodedoesNotExist_ThenResultContainsInternalErrorCode_2()
        {
            //Arrange
            var errorCode = 10000000;

            //Act
            var result = new WebErrorLocalize()[errorCode];

            //Assert
            Assert.IsTrue(result.Contains($"Неизвестный код ошибки '{errorCode}'"));
        }
    }
}
