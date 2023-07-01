namespace ServerAPIBase
{
    public interface ICodeReceiver<T> : IWebApiRequester<ICodeReceiverData, T>
    {

    }
}

public interface ICodeReceiverData
{
    string TaskID { get; }
    string Token { get; }
}

public class CodeReceiverData : ICodeReceiverData
{
    public string TaskID { get; }
    public string Token { get; }

    public CodeReceiverData(string taskID, string token)
    {
        TaskID = taskID;
        Token = token;
    }
}