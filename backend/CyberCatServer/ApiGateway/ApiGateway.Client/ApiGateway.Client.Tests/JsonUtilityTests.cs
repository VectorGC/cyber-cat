#if UNITY_WEBGL
using NUnit.Framework;
using Shared.Models.Dto;
using UnityEngine;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class JsonUtilityTests
    {
        [Test]
        public void DeserializeTaskDtoByJsonUtility_WhenPassCorrectJson()
        {
            var json = "{\"name\":\"Hello cat!\",\"description\":\"Вывести строку: Hello cat!\"}";
            var task = JsonUtility.FromJson<TaskDto>(json);

            Assert.IsNotNull(task);
            Assert.IsNotEmpty(task.Name);
        }
    }
}
#endif