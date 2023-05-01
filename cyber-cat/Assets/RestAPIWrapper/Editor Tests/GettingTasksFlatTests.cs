using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using TaskUnits;
using UnityEngine;

namespace RestAPIWrapper.EditorTests
{
    public class GettingTasksFlatTests
    {
        [Test]
        public async Task WhenGettingTasks_AndCorrectToken_ThenListIsNotEmpty()
        {
            //Arrange
            var playerToken = PlayerPrefs.GetString("token");

            //Act
            var jsonToken = await RestAPI.Instance.GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public async Task WhenGettingTasks_AndCorrectTokenAndServerless_ThenListIsNotEmpty()
        {
            //Arrange
            var playerToken = PlayerPrefs.GetString("token");

            //Act
            var jsonToken = await new Serverless.RestAPIServerless().GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public async Task WhenGettingTasks_AndCorrectTokenAndServer_ThenListIsNotEmpty()
        {
            //Arrange
            var playerToken = PlayerPrefs.GetString("token");

            //Act
            var jsonToken = await new Server.RestAPIServer().GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public async Task WhenGettingTasks_AndWrongToken_ThenListIsEmpty()
        {
            //Arrange
            var playerToken = "Absolutly wrong token";

            //Act
            var jsonToken = await RestAPI.Instance.GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
        }

        [Test]
        public async Task WhenGettingTasks_AndWrongTokenAndServerless_ThenListIsNotEmpty()
        {
            //Arrange
            var playerToken = "Absolutly wrong token";

            //Act
            var jsonToken = await RestAPI.Instance.GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public async Task WhenGettingTasks_AndWrongTokenAndServer_ThenListIsEmpty()
        {
            //Arrange
            var playerToken = "Absolutly wrong token";

            //Act
            var jsonToken = await new Server.RestAPIServer().GetTasks(playerToken);
            var list = JsonConvert.DeserializeObject<ITaskDataCollection>(jsonToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
        }
    }
}
