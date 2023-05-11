using Newtonsoft.Json.Linq;
using ServerAPIBase;
using TaskUnits;

namespace RestAPIWrapper
{
    public interface IRestAPIWrapper : IRestAPI<TokenSession, string, string, string, ITaskDataCollection, JObject, ICodeConsoleMessage>
    {

    }

    public class TokenSession
    {
        
    }
}

