using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;
using ServerAPIBase;
using TaskUnits;

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
            var list = await RestAPI.Instance.GetTasks(playerToken);

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
            var list = await new Serverless.RestAPIServerless().GetTasks(playerToken);

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
            var list = await new V1.RestAPIV1().GetTasks(playerToken);

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
            var list = await RestAPI.Instance.GetTasks(playerToken);

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
            var list = await RestAPI.Instance.GetTasks(playerToken);

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
            var list = await new V1.RestAPIV1().GetTasks(playerToken);

            //Assert
            Assert.IsNotNull(list);
            Assert.IsEmpty(list);
        }
    }
}
