using System;
using Cysharp.Threading.Tasks;
using GameCodeEditor.Scripts;
using Legacy_do_not_use_it;
using TasksData;
using UniRx;

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
            .Do(taskData => _taskData = taskData)
            .AsUnitObservable();

        observable.Subscribe();

        return observable;
    }

    public async UniTask<ITaskData> GetTask(string token, IProgress<float> progress = null) =>
        await _taskUnitFolder.GetTask(token, progress);

    public async UniTask<bool?> IsTaskSolved(string token, IProgress<float> progress = null) =>
        await _taskUnitFolder.IsTaskSolved(token, progress);

    public string Id => _taskData.Id;
    public string Name => _taskData.Name;
    public string Description => _taskData.Description;
    public string Output => _taskData.Output;
    public bool? IsSolved => _taskData.IsSolved;

    public IDisposable Subscribe(IObserver<ITaskData> observer) => _subject.Subscribe(observer);
}