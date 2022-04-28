using System;
using Cysharp.Threading.Tasks;
using TasksData;
using TaskUnits.Messages;
using UniRx;

namespace TaskUnits.TasksData
{
    internal class TaskObservable : IObservable<ITaskData>
    {
        private readonly string _taskId;
        private readonly Subject<ITaskData> _subject = new Subject<ITaskData>();

        internal TaskObservable(ITaskData taskData)
        {
            _taskId = taskData.Id;

            AsyncMessageBroker.Default.Subscribe<NeedUpdateTaskData>(OnNeedUpdateTaskData);
        }

        private IObservable<Unit> OnNeedUpdateTaskData(NeedUpdateTaskData message)
        {
            if (message.TaskId != _taskId)
            {
                return Observable.ReturnUnit();
            }

            var token = message.Token;
            var observable = TaskFacade.GetTask(token, message.TaskId)
                .ToObservable()
                .Do(CallNotifyTaskUpdate)
                .AsUnitObservable();

            observable.Subscribe();

            return observable;
        }

        private void CallNotifyTaskUpdate(ITaskData taskData)
        {
            _subject.OnNext(taskData);
            if (taskData.IsSolved == true)
            {
                _subject.OnCompleted();
            }

            if (taskData is EmptyTaskData)
            {
                var ex = new Exception($"Not found task from folder '{this}'. Maybe server error");
                _subject.OnError(ex);

                throw ex;
            }
        }

        public IDisposable Subscribe(IObserver<ITaskData> observer)
        {
            var unsubscriber = _subject.Subscribe(observer);
            return unsubscriber;
        }
    }
}