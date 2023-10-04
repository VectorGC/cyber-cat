using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiGateway.Client.Internal.Tasks.Statuses;
using ApiGateway.Client.Internal.Tasks.Verdicts;
using ApiGateway.Client.Models;
using Shared.Models.Data;
using Shared.Models.Enums;
using Shared.Models.Ids;
using Shared.Models.Models;

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

        public Task<IVerdict> VerifySolution(string sourceCode)
        {
            _solution = sourceCode;
            _verifyCount++;

            switch (_verifyCount)
            {
                case 1:
                    return Task.FromResult<IVerdict>(new Failure(new VerdictData()
                    {
                        Error = "Это режим без сервера. Отправьте задачу на проверку ещё раз, чтобы решение было успешным.",
                        Status = VerdictStatus.Failure
                    }));

                default:
                    return Task.FromResult<IVerdict>(new Success(new VerdictData()
                    {
                        Status = VerdictStatus.Success
                    }));
            }
        }

        public Task<VerdictV2> VerifySolutionV2(string sourceCode)
        {
            var testCases = new FailureV2()
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

            return Task.FromResult<VerdictV2>(testCases);
        }
    }
}