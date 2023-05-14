using System;
using Cysharp.Threading.Tasks;
using Models;
using ServerAPI;

internal class MockServerAPI : IServerAPI
{
    public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }

    UniTask<ITasks> IServerAPI.GetTasks(IProgress<float> progress)
    {
        throw new NotImplementedException();
    }

    public UniTask<IPlayer> AuthorizePlayer(ITokenSession token)
    {
        throw new NotImplementedException();
    }

    public UniTask<IPlayer> AuthorizePlayer(ITokenSession token, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }

    public UniTask<string> GetTasks(IProgress<float> progress = null)
    {
        // TODO: Хз как сделать красивее.
        return UniTask.FromResult("{" +
                                  "\"tasks\" : " +
                                  "{" +
                                  "\"0\" : " +
                                  "{" +
                                  "\"name\": \"stub_task\"" +
                                  "}" +
                                  "}" +
                                  "}");
    }
}