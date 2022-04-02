using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

public class Tasks
{
    public static async UniTask<ITaskTicket> GetTask(string taskId)
    {
        var token = TokenSession.FromPlayerPrefs();
        return await RestAPI.GetTask(token, taskId);
    }
}