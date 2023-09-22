using ApiGateway.Client.Models;

namespace ApiGateway.Client.Internal.Serverless
{
    internal class TestCaseServerless : ITestCase
    {
        public object[] Inputs { get; }
        public object Expected { get; }

        public TestCaseServerless(object[] inputs, object expected)
        {
            Inputs = inputs;
            Expected = expected;
        }
    }
}