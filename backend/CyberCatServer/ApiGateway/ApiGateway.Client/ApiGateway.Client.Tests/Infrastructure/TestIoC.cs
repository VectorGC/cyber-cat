using System;
using ApiGateway.Client.Application;
using NUnit.Framework;

namespace ApiGateway.Client.Tests
{
    interface ITest1
    {
        void Print();
    }

    class ClassTest1 : ITest1
    {
        public void Print()
        {
            Console.WriteLine("ClassName: {0}, HashCode: {1}", this.GetType().Name, this.GetHashCode());
        }
    }

    interface ITest2
    {
        void Print();
    }

    class ClassTest2 : ITest2
    {
        private ITest1 _test1;

        public ClassTest2(ITest1 test1)
        {
            _test1 = test1;
        }
        public void Print()
        {
            Console.WriteLine("ClassName: {0}, HashCode: {1}", this.GetType().Name, this.GetHashCode());
        }
    }

    [TestFixture]
    public class TestIoC
    {
        [Test]
        public void Test1()
        {
            var container = new TinyIoCContainer();
            container.Register<ITest1, ClassTest1>().AsSingleton();
            container.Register<ITest2, ClassTest2>();

            var test = container.Resolve<ITest2>();
            Assert.NotNull(test);
        }
    }
}