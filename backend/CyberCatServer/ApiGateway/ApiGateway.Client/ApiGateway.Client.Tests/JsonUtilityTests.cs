#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;
using UnityEngine;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class JsonUtilityTests
    {
        [Serializable]
        private class ListModel
        {
            public List<string> list;
        }

        [Serializable]
        public class ListOfTaskIds
        {
            public List<TaskId> taskIds;
        }

        [Test]
        public void DeserializeModel_WhenPassCorrectJson()
        {
            var json = "{\"name\":\"Hello cat!\",\"description\":\"Вывести строку: Hello cat!\"}";
            var task = JsonUtility.FromJson<TaskDescription>(json);

            Assert.IsNotNull(task);
            Assert.IsNotEmpty(task.Name);
        }

        [Test]
        public void DeserializeModelListOfStrings_WhenPassCorrectJson()
        {
            var json = "{\"list\":[\"one\", \"two\", \"three\"]}";

            var model = JsonUtility.FromJson<ListModel>(json);

            Assert.That(model.list, Does.Contain("one"));
            Assert.That(model.list, Does.Contain("two"));
            Assert.That(model.list, Does.Contain("three"));
        }

        [Test]
        public void NotSupportedModelListOfObject_WhenPassCorrectJson()
        {
            var json = "{\"taskIds\":[{\"value\":\"one\"}, {\"value\":\"two\"}, {\"value\":\"three\"}]}";

            var model = JsonUtility.FromJson<ListOfTaskIds>(json);

            Assert.IsNull(model.taskIds);
        }

        [Test]
        public void NotSupportedCollectionOfStrings_WhenPassCorrectJson()
        {
            var json = "[\"one\", \"two\", \"three\"]";

            var ex = Assert.Throws<ArgumentException>(() => JsonUtility.FromJson<string[]>(json));
            Assert.AreEqual(ex.Message, "JSON must represent an object type.");

            ex = Assert.Throws<ArgumentException>(() => JsonUtility.FromJson<List<string>>(json));
            Assert.AreEqual(ex.Message, "JSON must represent an object type.");
        }
    }
}
#endif