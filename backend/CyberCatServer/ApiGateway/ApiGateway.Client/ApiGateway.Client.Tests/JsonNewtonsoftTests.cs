using System;
using System.Collections.Generic;
using fastJSON;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    [TestFixture]
    public class JsonNewtonsoftTests
    {
        public class SimpleModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> SimpleList { get; set; }
            public string[] SimpleArray { get; set; }
            public Dictionary<string, string> SimpleDictionary { get; set; }
            public Dictionary<string, object> SimpleObjectDictionary { get; set; }

            public void AssertAreEqual(SimpleModel other)
            {
                Assert.AreEqual(Name, other.Name);
                Assert.AreEqual(Description, other.Description);
                Assert.AreEqual(SimpleList, other.SimpleList);
                Assert.AreEqual(SimpleDictionary, other.SimpleDictionary);
                Assert.AreEqual(SimpleObjectDictionary, other.SimpleObjectDictionary);
            }
        }

        public class BaseModel
        {
            public SimpleModel Model { get; set; }
            public List<SimpleModel> List { get; set; }
            public SimpleModel[] Array { get; set; }
            public Dictionary<string, SimpleModel> Dictionary { get; set; }

            // Struct key dictionary not supported.
            // public Dictionary<SimpleStruct, SimpleModel> StructDictionary { get; set; }

            public void AssertAreEqual(BaseModel other)
            {
                Model.AssertAreEqual(Model);
                for (var i = 0; i < List.Count; i++)
                {
                    List[i].AssertAreEqual(other.List[i]);
                }

                for (var i = 0; i < Array.Length; i++)
                {
                    Array[i].AssertAreEqual(other.Array[i]);
                }

                foreach (var key in Dictionary.Keys)
                {
                    Dictionary[key].AssertAreEqual(other.Dictionary[key]);
                }
            }
        }

        public struct SimpleStruct
        {
            public string Value { get; set; }
        }

        public interface ISome
        {
            string Value { get; set; }
            void AssertAreEqual(ISome other);
        }


        public abstract class SomeBase : ISome
        {
            public abstract string Value { get; set; }
            public abstract void AssertAreEqual(ISome other);
        }

        public class SomeHandler
        {
            public ISome Some { get; set; }
            public SomeBase SomeBase { get; set; }

            public void AssertAreEqual(SomeHandler other)
            {
                Some.AssertAreEqual(other.Some);
                SomeBase.AssertAreEqual(other.SomeBase);
            }
        }

        public class Some1 : SomeBase
        {
            public override string Value { get; set; }

            public override void AssertAreEqual(ISome other)
            {
                Assert.AreEqual(Value, other.Value);
            }
        }

        public class Some2 : SomeBase
        {
            public override string Value { get; set; }

            public override void AssertAreEqual(ISome other)
            {
                Assert.AreEqual(Value, other.Value);
            }
        }


        [Test]
        public void DeserializeSimpleModel_WhenPassCorrectJson()
        {
            var json = "{\"name\":\"Hello cat!\",\"description\":\"Вывести строку: Hello cat!\"}";
            var json2 = "{\"Name\":\"Hello cat!\",\"Description\":\"Вывести строку: Hello cat!\"}";


            var simpleModel = JSON.ToObject<SimpleModel>(json);
            var simpleModel2 = JSON.ToObject<SimpleModel>(json2);

            Assert.IsNotNull(simpleModel);
            Assert.IsNotEmpty(simpleModel.Name);
            Assert.IsNotEmpty(simpleModel.Description);

            Assert.IsNotNull(simpleModel2);
            Assert.IsNotEmpty(simpleModel2.Name);
            Assert.IsNotEmpty(simpleModel2.Description);
        }

        [Test]
        public void SerializeAndDeserializeSimpleModel()
        {
            var simpleModel = GetMockSimpleModel();

            var json = JSON.ToJSON(simpleModel);
            var simpleModelAfterDeserialize = JSON.ToObject<SimpleModel>(json);

            simpleModel.AssertAreEqual(simpleModelAfterDeserialize);
        }

        // Struct not supported.
        /*
        [Test]
        public void SerializeAndDeserializeStructures()
        {
            var structure = new SimpleStruct()
            {
                Value = "123"
            };

            var json = JSON.ToJSON(structure);
            var structureAfterDeserialize = JSON.ToObject<SimpleStruct>(json);

            Assert.AreEqual(structure.Value, structureAfterDeserialize.Value);
        }
        */

        [Test]
        public void SerializeAndDeserializeNestedObjects()
        {
            var baseModel = new BaseModel()
            {
                Model = GetMockSimpleModel(),
                List = new List<SimpleModel>()
                {
                    GetMockSimpleModel(),
                    GetMockSimpleModel()
                },
                Array = new SimpleModel[]
                {
                    GetMockSimpleModel(),
                    GetMockSimpleModel(),
                },
                Dictionary = new Dictionary<string, SimpleModel>()
                {
                    ["1"] = GetMockSimpleModel(),
                    ["2"] = GetMockSimpleModel()
                },
            };

            var json = JSON.ToJSON(baseModel);
            var baseModelAfterDeserialize = JSON.ToObject<BaseModel>(json);

            baseModel.AssertAreEqual(baseModelAfterDeserialize);
        }

        [Test]
        public void SerializeAndDeserializePolymorphic()
        {
            var someHandler = new SomeHandler()
            {
                Some = GetSomeMock(),
                SomeBase = GetSomeMock()
            };

            var json = JSON.ToJSON(someHandler);
            var someHandlerAfterDeserialize = JSON.ToObject<SomeHandler>(json);

            someHandler.AssertAreEqual(someHandlerAfterDeserialize);
        }

        private SimpleModel GetMockSimpleModel()
        {
            return new SimpleModel()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                SimpleList = new List<string>() {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                SimpleArray = new string[] {Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()},
                SimpleDictionary = new Dictionary<string, string>()
                {
                    ["1"] = "1",
                    ["2"] = "2",
                    ["3"] = "3",
                },
                SimpleObjectDictionary = new Dictionary<string, object>()
                {
                    ["1"] = 1,
                    ["2"] = 2f,
                    ["3"] = "3"
                }
            };
        }

        private SimpleStruct GetMockStruct(string value)
        {
            return new SimpleStruct()
            {
                Value = value
            };
        }

        private SomeBase GetSomeMock()
        {
            var rand = new Random();
            if (rand.Next() % 2 == 0)
            {
                return new Some1()
                {
                    Value = Guid.NewGuid().ToString()
                };
            }
            else
            {
                return new Some2()
                {
                    Value = Guid.NewGuid().ToString()
                };
            }
        }
    }
}