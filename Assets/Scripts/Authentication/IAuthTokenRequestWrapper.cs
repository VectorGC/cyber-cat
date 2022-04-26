using System;
using Cysharp.Threading.Tasks;

namespace Authentication
{
    public interface IAuthTokenRequestWrapper
    {
        UniTask<TokenSession> GetAuthData(string login, string password, IProgress<float> progress = null);
        UniTask RegisterUser(string login, string password, string name);
        UniTask RestorePassword(string login, string password);
    }
}