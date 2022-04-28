using System;
using TaskUnits.TasksData;

namespace TaskUnits
{
    public static class TaskDataExt
    {
        public static IObservable<ITaskData> ToObservable(this ITaskData taskData)
        {
            return new TaskObservable(taskData);
        }
    }
}