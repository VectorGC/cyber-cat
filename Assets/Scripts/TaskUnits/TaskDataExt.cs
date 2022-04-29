using System;
using TaskUnits.TaskDataModels;
using UniRx;

namespace TaskUnits
{
    public static class TaskDataExt
    {
        public static IObservable<ITaskData> ToObservable(this ITaskData taskData)
        {
            // All observers receivers task data on subscribe.
            return new TaskObservable(taskData).StartWith(taskData);
        }
    }
}