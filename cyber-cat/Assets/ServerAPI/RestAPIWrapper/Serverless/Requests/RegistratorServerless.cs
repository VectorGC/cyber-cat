using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.Serverless
{
    public class RegistratorServerless : IRegistrator<string>
    {
        public void Request(IRegistratorData data, Action<string> callback)
        {
            const string messageText =
                "Это режим 'без сервера', чтобы активировать регистрацию - пожалуйста включите сервер";
            callback?.Invoke(messageText);
        }

        public async UniTask<string> RequestAsync(IRegistratorData data, IProgress<float> progress = null)
        {
            return await UniTask.FromResult(string.Empty);
        }
    }
}