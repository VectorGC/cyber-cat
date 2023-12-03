using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Tests.Extensions;
using ApiGateway.Client.V3.Application;
using NUnit.Framework;
using Shared.Models.Domain.Verdicts;

namespace ApiGateway.Client.Tests
{
    public class SubmitSolutionTests : ApiGatewayClientTestFixture
    {
        private IApiGatewayClient _client;

        public SubmitSolutionTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _client = GetTestPlayerClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";

            var result = await _client.Player.Tasks[taskId].SubmitSolution(sourceCode);

            var success = result.Value as Success;
            Assert.AreEqual(1, success.TestCases.PassedCount);
        }

        [Test]
        public async Task FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h> \nint main()";
            var expectedErrorRegex = "Exit Code 1:.*: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";

            var result = await _client.Player.Tasks[taskId].SubmitSolution(sourceCode);

            var failure = result.Value as NativeFailure;
            Assert.That(failure.Error, Does.Match(expectedErrorRegex));
        }

        [Test]
        public async Task FailureVerifyHelloCatTaskWithError_WhenPassInfinityLoopCode()
        {
            var taskId = "tutorial";
            var sourceCode = "int main() { while(true){} }";
            var expectedErrorRegex = "Exit Code .*: The process took more than 3 seconds";

            var result = await _client.Player.Tasks[taskId].SubmitSolution(sourceCode);

            var failure = result.Value as NativeFailure;
            Assert.That(failure.Error, Does.Match(expectedErrorRegex));
        }

        [Test]
        [Ignore("WebClient does not support parallel operations. Use HttpClient for this purpose")]
        public async Task CompileAndLaunchManyProcess_WithDifferentResult()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < 5; i++)
            {
                tasks.Add(SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode());
            }

            for (var i = 0; i < 5; i++)
            {
                tasks.Add(FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode());
            }

            for (var i = 0; i < 5; i++)
            {
                tasks.Add(FailureVerifyHelloCatTaskWithError_WhenPassInfinityLoopCode());
            }

            await Task.WhenAll(tasks);
        }

        [Test]
        public async Task FailureWithInput_WhenPassNotAllTests()
        {
            const string taskId = "task-1";
            // We simply output the result of the first test. So that the first test passes, while the rest fail.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; scanf(\"%d%d\", &a, &b); printf(\"2\"); }";

            var result = await _client.Player.Tasks[taskId].SubmitSolution(sourceCode);
            var verdict = result.Value;

            Assert.IsAssignableFrom<Failure>(verdict);
            var failure = verdict as Failure;
            Assert.AreEqual(1, failure.TestCases.PassedCount);

            var testCase1 = failure.TestCases[taskId, 0] as SuccessTestCaseVerdict;
            var testCase2 = failure.TestCases[taskId, 1] as FailureTestCaseVerdict;
            var testCase3 = failure.TestCases[taskId, 2] as FailureTestCaseVerdict;

            Assert.AreEqual("2", testCase1.Output);
            Assert.AreEqual("Expected result '15', but was '2'", testCase2.Error);
            Assert.AreEqual("Expected result '0', but was '2'", testCase3.Error);
        }

        [Test]
        public async Task Failure_WhenWaitInputInfinity()
        {
            const string taskId = "task-1";
            // We made an extra input and are waiting indefinitely for 'c' to be entered.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; int c; scanf(\"%d%d\", &a, &b); scanf(\"%d\", &c); }";
            var expectedErrorRegex = "Exit Code .*: The process took more than 3 seconds";

            var result = await _client.Player.Tasks[taskId].SubmitSolution(sourceCode);

            var failure = result.Value as NativeFailure;
            Assert.That(failure.Error, Does.Match(expectedErrorRegex));
        }
    }
}