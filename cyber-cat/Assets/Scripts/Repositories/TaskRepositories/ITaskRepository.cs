using System;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;

namespace Repositories.TaskRepositories
{
    public interface ITaskRepository
    {
        public UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null);
    }
}