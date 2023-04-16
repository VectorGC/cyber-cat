using System.Threading.Tasks;
using Authentication;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RestAPIWrapper.EditorTests
{
    [TestFixture]
    public class CodeSendingTests
    {
        [Test]
        public async Task WhenSendCodeToChecking_AndServerless_ThenErrorIsNotEmptyAndTokenIsNull()
        {
            //Arrange. Подготовка данных
            var playerToken = "";
            var taskID = "";
            var code = "";
            var lang = "";

            //Act
            var jsonToken = await new Serverless.RestAPIServerless()
                .SendCodeToChecking(playerToken, taskID, code, lang);
            var token = JsonConvert.DeserializeObject<TokenSession>(jsonToken);

            //Assert
            Assert.IsNull(token.Token);
            Assert.IsNotEmpty(token.Error);
        }

        [RequiresPlayMode]
        [Test]
        public async Task WhenSendCodeToChecking_AndCorrectValues_ThenErrorIsEmptyAndNameIsNotEmpty()
        {
            //Arrange
            var playerToken = PlayerPrefs.GetString("token");
            var taskID = "17"; //Проверка на простое число
            var code = "#include <iostream>\r\n\r\nint IsSimple(int N);\r\n\r\nint main()\r\n{\r\n    int N = 0;\r\n    scanf(\"%d\", &N);\r\n    if (IsSimple(N))\r\n    {\r\n        printf(\"true\");\r\n    }\r\n    else\r\n    {\r\n        printf(\"false\");\r\n    }\r\n    printf(\"\\n\");\r\n}\r\n\r\nint IsSimple(int N)\r\n{\r\n    double sqr = sqrt(N);\r\n    if (N == 0 || N == 1 || N == 2)\r\n    {\r\n        return 1;\r\n    }\r\n    for (int i = 2; i <= sqr; i++)\r\n    {\r\n        if (N % i == 0)\r\n        {\r\n            return 0;\r\n        }\r\n    }\r\n    return 1;\r\n}";
            var lang = "Cpp";

            //Act
            var jsonToken = await RestAPI.Instance.SendCodeToChecking(playerToken, taskID, code, lang);
            var token = JsonConvert.DeserializeObject<TokenSession>(jsonToken);

            //Assert
            Assert.IsNotNull(token.Name);
            Assert.IsNotNull(token.Token);
            Assert.IsEmpty(token.Error);
        }

        [RequiresPlayMode]
        [Test]
        public async Task WhenSendCodeToChecking_AndCorrectValuesAndServer_ThenErrorIsEmptyAndNameIsNotEmpty()
        {
            //Arrange
            var playerToken = PlayerPrefs.GetString("token");
            var taskID = "17"; //Проверка на простое число
            var code = "#include <iostream>\r\n\r\nint IsSimple(int N);\r\n\r\nint main()\r\n{\r\n    int N = 0;\r\n    scanf(\"%d\", &N);\r\n    if (IsSimple(N))\r\n    {\r\n        printf(\"true\");\r\n    }\r\n    else\r\n    {\r\n        printf(\"false\");\r\n    }\r\n    printf(\"\\n\");\r\n}\r\n\r\nint IsSimple(int N)\r\n{\r\n    double sqr = sqrt(N);\r\n    if (N == 0 || N == 1 || N == 2)\r\n    {\r\n        return 1;\r\n    }\r\n    for (int i = 2; i <= sqr; i++)\r\n    {\r\n        if (N % i == 0)\r\n        {\r\n            return 0;\r\n        }\r\n    }\r\n    return 1;\r\n}";
            var lang = "Cpp";

            //Act
            var jsonToken = await RestAPI.Instance.SendCodeToChecking(playerToken, taskID, code, lang);
            var token = JsonConvert.DeserializeObject<TokenSession>(jsonToken);

            //Assert
            Assert.IsNotNull(token.Name);
            Assert.IsNotNull(token.Token);
            Assert.IsEmpty(token.Error);
        }
    }
}
