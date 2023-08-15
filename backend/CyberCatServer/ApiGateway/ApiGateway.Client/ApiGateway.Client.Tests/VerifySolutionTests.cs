using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Factory;
using ApiGateway.Client.Tests.Abstracts;
using NUnit.Framework;
using Shared.Models.Models;

namespace ApiGateway.Client.Tests
{
    public class VerifySolutionTests : AuthorizedClientTestFixture
    {
        public VerifySolutionTests(ServerEnvironment serverEnvironment) : base(serverEnvironment)
        {
        }

        [Test]
        public async Task SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
            var client = await GetClient();

            var verdict = await client.JudgeService.VerifySolution(taskId, sourceCode);

            Assert.IsTrue(string.IsNullOrEmpty(verdict.Error));
            Assert.AreEqual(VerdictStatus.Success, verdict.Status);
            Assert.AreEqual(1, verdict.TestsPassed);
        }

        [Test]
        public async Task FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h> \nint main()";
            var expectedErrorRegex = "Exit Code 1:.*: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";
            var client = await GetClient();

            var verdict = await client.JudgeService.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(0, verdict.TestsPassed);
            Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
        }

        [Test]
        public async Task FailureVerifyHelloCatTaskWithError_WhenPassInfinityLoopCode()
        {
            var taskId = "tutorial";
            var sourceCode = "int main() { while(true){} }";
            var expectedErrorRegex = "Exit Code .*: The process took more than 2 seconds";
            var client = await GetClient();

            var verdict = await client.JudgeService.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(0, verdict.TestsPassed);
            Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
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
            const string taskId = "sum_ab";
            // We simply output the result of the first test. So that the first test passes, while the rest fail.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; scanf(\"%d%d\", &a, &b); printf(\"2\"); }";
            var client = await GetClient();

            var verdict = await client.JudgeService.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(1, verdict.TestsPassed);
            Assert.AreEqual("Expected result '15', but was '2'", verdict.Error);
        }

        [Test]
        public async Task Failure_WhenWaitInputInfinity()
        {
            const string taskId = "sum_ab";
            // We made an extra input and are waiting indefinitely for 'c' to be entered.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; int c; scanf(\"%d%d\", &a, &b); scanf(\"%d\", &c); }";
            var expectedErrorRegex = "Exit Code .*: The process took more than 2 seconds";
            var client = await GetClient();

            var verdict = await client.JudgeService.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(0, verdict.TestsPassed);
            Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
        }
    }
}