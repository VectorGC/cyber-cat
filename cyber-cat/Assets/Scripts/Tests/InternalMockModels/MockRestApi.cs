using System;
using Cysharp.Threading.Tasks;
using Models;
using RestAPI;

internal class MockRestApi : IRestAPI
{
    public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
    {
        throw new NotImplementedException();
    }

    UniTask<ITasks> IRestAPI.GetTasks(IProgress<float> progress)
    {
        throw new NotImplementedException();
    }

    public UniTask<IPlayer> AuthorizeAsPlayer(ITokenSession token)
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