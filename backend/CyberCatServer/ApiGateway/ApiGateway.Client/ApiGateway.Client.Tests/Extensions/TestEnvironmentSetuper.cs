using System;
using Shared.Models.Domain.Tasks;

namespace ApiGateway.Client.Tests.Extensions
{
    public class TestEnvironmentSetuper : IDisposable
    {
        public void AddSharedTask(TaskId taskId, string exepected)
        {
        }

        public void RemoveSharedTask(TaskId taskId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}