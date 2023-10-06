using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Models;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using Shared.Models.Models.Verdicts;

namespace ApiGateway.Client.Internal.Serverless
{
    internal class TaskServerless : ITask
    {
        private int _verifyCount;
        private string _solution;

        public Task<string> GetName()
        {
            return Task.FromResult("Обучение. Режим без сервера.");
        }

        public Task<string> GetDescription()
        {
            return Task.FromResult("Вы играете в режиме без сервера. Первая проверка задачи - всегда ошибка. Вторая проверка - всегда успех.");
        }

        public Task<string> GetDefaultCode()
        {
            return Task.FromResult("Вы играете в режиме без сервера. Невозможно получить код по умолчанию");
        }

        public Task<ITaskProgressStatus> GetStatus()
        {
            switch (_verifyCount)
            {
                case 0:
                    return Task.FromResult<ITaskProgressStatus>(new NotStarted());
                case 1:
                    return Task.FromResult<ITaskProgressStatus>(new HaveSolution(_solution));
                default:
                    return Task.FromResult<ITaskProgressStatus>(new Complete(_solution));
            }
        }

        public Task<TestCases> GetTestCases()
        {
            var testCases = new TestCases()
            {
                Values = new Dictionary<TestCaseId, TestCase>()
                {
                    [new TestCaseId("tutorial", 0)] = new TestCase()
                    {
                        Id = new TestCaseId("tutorial", 0),
                        Expected = "Hello cat!"
                    },
                    [new TestCaseId("tutorial", 1)] = new TestCase()
                    {
                        Id = new TestCaseId("tutorial", 1),
                        Expected = "Hello mat!"
                    },
                    [new TestCaseId("tutorial", 2)] = new TestCase()
                    {
                        Id = new TestCaseId("tutorial", 2),
                        Inputs = new[] {"1", "2"},
                        Expected = "Hello bred!"
                    }
                }
            };

            return Task.FromResult(testCases);
        }

        public Task<Verdict> VerifySolution(string sourceCode)
        {
            var testCases = new Failure()
            {
                TestCases = new TestCasesVerdict()
                {
                    Values = new Dictionary<TestCaseId, TestCaseVerdict>
                    {
                        [new TestCaseId("tutorial", 0)] = new SuccessTestCaseVerdict()
                        {
                            TestCase = new TestCase()
                            {
                                Id = new TestCaseId("tutorial", 0),
                                Expected = "Hello cat!"
                            },
                            Output = "Hello cat!"
                        },
                        [new TestCaseId("tutorial", 1)] = new FailureTestCaseVerdict()
                        {
                            TestCase = new TestCase()
                            {
                                Id = new TestCaseId("tutorial", 1),
                                Expected = "Hello mat!"
                            },
                            Output = "Hello cat!"
                        },
                        [new TestCaseId("tutorial", 2)] = new FailureTestCaseVerdict()
                        {
                            TestCase = new TestCase()
                            {
                                Id = new TestCaseId("tutorial", 2),
                                Inputs = new[] {"1", "2"},
                                Expected = "Hello bred!"
                            },
                            Output = "Hello cat!"
                        }
                    }
                }
            };

            return Task.FromResult<Verdict>(testCases);
        }
    }
}