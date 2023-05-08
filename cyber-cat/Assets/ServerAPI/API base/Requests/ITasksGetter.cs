namespace ServerAPIBase
{
    public interface ITasksGetter<T> : IWebApiRequester<ITasksGetterData, T>
    {

    }

    public interface ITasksGetterData
    {
        string Token { get; }
    }

    public class TasksGetterData : ITasksGetterData
    {
        public string Token { get; }

        public TasksGetterData(string token)
        {
            Token = token;
        }
    }
}