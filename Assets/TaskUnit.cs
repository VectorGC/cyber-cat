using System;
using Cysharp.Threading.Tasks;
using GameCodeEditor.Scripts;
using Legacy_do_not_use_it;
using TasksData;
using UniRx;
using UnityEngine;

public class TaskUnit : ITaskUnit, ITaskData, IObservable<ITaskData>
{
    private ITaskData _taskData;

    private readonly TaskUnitFolder _taskUnitFolder;
    private readonly Subject<ITaskData> _subject = new Subject<ITaskData>();

    public static async UniTask<TaskUnit> CreateTaskUnit(string token, TaskUnitFolder taskUnitFolder,
        IProgress<float> progress = null)
    {
        var taskData = await taskUnitFolder.GetTask(token, progress);
        var taskUnit = new TaskUnit(taskData, taskUnitFolder);

        return taskUnit;
    }

    private TaskUnit(ITaskData taskData, TaskUnitFolder taskUnitFolder)
    {
        _taskData = taskData;
        _taskUnitFolder = taskUnitFolder;

        AsyncMessageBroker.Default.Subscribe<NeedUpdateTaskData>(OnNeedUpdateTaskData);
    }

    private IObservable<Unit> OnNeedUpdateTaskData(NeedUpdateTaskData message)
    {
        if (message.TaskId != Id)
        {
            return Observable.ReturnUnit();
        }

        var token = message.Token;
        var observable = GetTask(token)
            .ToObservable()
            .Do(taskData =>
            {
                _taskData = taskData;
                CallNotifyTaskUpdate(taskData);
            })
            .AsUnitObservable();

        observable.Subscribe();

        return observable;
    }

    private void CallNotifyTaskUpdate(ITaskData taskData)
    {
        _subject.OnNext(this);
        if (IsSolved == true)
        {
            _subject.OnCompleted();
        }

        if (taskData == null || taskData is EmptyTaskData)
        {
            Debug.LogError($"Not found task from folder '{this}'");
            _subject.OnError(new Exception());
        }
    }

    public override string ToString() => _taskUnitFolder.ToString();

    public async UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null) =>
        await _taskUnitFolder.GetTask(token, progress);

    public string Id => _taskData.Id;
    public string Name => _taskData.Name;
    public string Description => _taskData.Description;
    public string Output => _taskData.Output;

    public bool? IsSolved
    {
        get
        {
            if (_taskUnitFolder.Unit == "unit-0")
            {
                return false;
            }

            return _taskData?.IsSolved;
        }
    } 
    
    public float ReceivedScore => _taskData.ReceivedScore;
    public float TotalScore => _taskData.TotalScore;

    public IDisposable Subscribe(IObserver<ITaskData> observer)
    {
        var unsubscriber = _subject.Subscribe(observer);
        CallNotifyTaskUpdate(_taskData);

        return unsubscriber;
    }
}