using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions.DotNetExt;
using TasksData;

namespace RestAPIWrapper
{
    public readonly struct GetTaskRequest
    {
        private readonly string _taskId;
        private readonly string _token;

        public GetTaskRequest(string taskId, string token)
        {
            _taskId = taskId;
            _token = token;
        }

        public async UniTask<ITaskData> SendRequest(IProgress<float> progress = null)
        {
            try
            {
                var tasks = await RestAPI.GetTasks(_token, progress);
                return tasks.GetValue(_taskId);
            }
            catch (KeyNotFoundException)
            {
                CodeConsole.WriteLine($"Задание '{_taskId}' не найдено в доступных заданиях", ConsoleMessageType.Error);
                throw;
            }
            catch (Exception ex)
            {
                CodeConsole.WriteLine(ex);
                throw;
            }
        }
    }
}