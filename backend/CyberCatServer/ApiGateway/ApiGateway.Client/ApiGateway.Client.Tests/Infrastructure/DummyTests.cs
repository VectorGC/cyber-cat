using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ApiGateway.Client.Tests.Infrastructure
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void Dummy()
        {
            Assert.Pass();
        }

        [Test]
        public void DummyAsync()
        {
            Task.Delay(TimeSpan.FromSeconds(0.1f));
            Assert.Pass();
        }
    }
}