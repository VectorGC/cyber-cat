using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest;
using AuthService;
using Newtonsoft.Json;
using NUnit.Framework;
using ServerAPI;
using Services.AuthService;
using Tests.InternalMockModels;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private readonly IAuthService _service = new AuthServiceProxy(ServerAPIFacade.Create());

        [Test]
        public async Task ShouldTokenIsNotEmpty_WhenCorrectCredentials([Values("test@test.com")] string login, [Values("test")] string password)
        {
            var token = await _service.Authenticate(login, password);

            Assert.IsNotEmpty(token.Value);
        }

        [Test]
        public async Task ShouldAuthorizeAsPlayer_WhenCorrectToken([Values("correct_token")] string token)
        {
            var player = await _service.AuthorizePlayer(new MockToken(token));

            Assert.IsNotEmpty(player.Name);
            Assert.AreEqual(token, player.Token.Value);
        }

        [Test]
        public void Serialize()
        {
            var str = "testString";
            //var json = JsonConvert.SerializeObject(str);
            var json = SerializeToJson(str);

            Assert.AreEqual("213", json);
        }

        [UnityTest]
        public IEnumerator PostStringAsync()
        {
            var sourceCode = "123213";
            var j = "{\"sourceCode\":\"12123\"}";
            var json = SerializeToJson(sourceCode);
            var f = new WWWForm();
            f.AddField("sourceCode", sourceCode);
            using (var request = UnityEngine.Networking.UnityWebRequest.Post("http://localhost:5000" + "/solution/tutorial", j))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
            }
        }

        private static string SerializeToJson<TValue>(TValue value)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(TValue));
                serializer.WriteObject(stream, value);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}