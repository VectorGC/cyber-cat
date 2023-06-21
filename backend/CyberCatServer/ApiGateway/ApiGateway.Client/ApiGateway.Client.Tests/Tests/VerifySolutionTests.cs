using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Shared.Models;
using Shared.Models.Models;

namespace ApiGateway.Client.Tests.Tests
{
    [TestFixture]
    public class VerifySolutionTests
    {
        [Test]
        public async Task SuccessVerifyHelloCatTaskWithoutOutput_WhenPassCorrectCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h>\nint main() { printf(\"Hello cat!\"); }";
            var client = await PlayerClient.Create();

            var verdict = await client.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Success, verdict.Status);
            Assert.IsNull(verdict.Error);
            Assert.AreEqual(1, verdict.TestsPassed);
        }

        [Test]
        public async Task FailureVerifyHelloCatTaskWithError_WhenPassNonCompileCode()
        {
            var taskId = "tutorial";
            var sourceCode = "#include <stdio.h> \nint main()";
            var expectedErrorRegex = "Exit Code 1:.*: error: expected initializer at end of input\n    2 | int main()\n      |           ^\n";
            var client = await PlayerClient.Create();

            var verdict = await client.VerifySolution(taskId, sourceCode);

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
            var client = await PlayerClient.Create();

            var verdict = await client.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(0, verdict.TestsPassed);
            Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
        }

        //[Test]
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
            // Просто выводим результат первого теста. Чтобы первый тест прошел, а остальные завалились.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; scanf(\"%d%d\", &a, &b); printf(\"2\"); }";
            var client = await PlayerClient.Create();

            var verdict = await client.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(1, verdict.TestsPassed);
            Assert.AreEqual("Expected result '15', but was '2'", verdict.Error);
        }

        [Test]
        public async Task Failure_WhenWaitInputInfinity()
        {
            const string taskId = "sum_ab";
            // Сделали лишний ввод, бесконечно ждем, когда введется 'c'.
            const string sourceCode = "#include <stdio.h>\nint main() { int a; int b; int c; scanf(\"%d%d\", &a, &b); scanf(\"%d\", &c); }";
            var expectedErrorRegex = "Exit Code .*: The process took more than 2 seconds";
            var client = await PlayerClient.Create();

            var verdict = await client.VerifySolution(taskId, sourceCode);

            Assert.AreEqual(VerdictStatus.Failure, verdict.Status);
            Assert.AreEqual(0, verdict.TestsPassed);
            Assert.That(verdict.Error, Does.Match(expectedErrorRegex));
        }
    }
}