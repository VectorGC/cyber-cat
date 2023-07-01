﻿using System.Threading.Tasks;
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
            var consoleMessage = await new Serverless.RestAPIServerless()
                .SendCodeToChecking(playerToken, taskID, code, lang);

            //Assert
            Assert.IsNotNull(consoleMessage.Message);
            Assert.IsNotEmpty(consoleMessage.Message);
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
            var consoleMessage = await RestAPI.Instance.SendCodeToChecking(playerToken, taskID, code, lang);

            //Assert
            Assert.IsNotNull(consoleMessage.Message);
            Assert.IsNotEmpty(consoleMessage.Message);
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
            var consoleMessage = await new V1.RestAPIV1().SendCodeToChecking(playerToken, taskID, code, lang);

            //Assert
            Assert.IsNotNull(consoleMessage.Message);
            Assert.IsNotEmpty(consoleMessage.Message);
        }
    }
}