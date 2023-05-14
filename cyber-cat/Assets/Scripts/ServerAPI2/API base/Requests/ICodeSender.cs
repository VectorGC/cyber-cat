using TaskUnits;

namespace ServerAPIBase
{
    public interface ICodeSender<T> : IWebApiRequester<ICodeSenderData, T>
    {

    }

    public interface ICodeSenderData
    {
        ITaskData TaskData { get; }
        string Token { get; }
        string Code { get; }
        string CodeLanguage { get; }
    }

    public class CodeSenderData : ICodeSenderData
    {
        public ITaskData TaskData { get; }
        public string Token { get; }
        public string Code { get; }
        public string CodeLanguage { get; }

        public CodeSenderData(ITaskData taskData, string token, string code, string codeLanguage)
        {
            TaskData = taskData;
            Token = token;
            Code = code;
            CodeLanguage = codeLanguage;
        }
    }
}