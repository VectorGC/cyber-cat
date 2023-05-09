using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.Serverless
{
    public class CodeSenderServerless : ICodeSender<string>
    {
        public void Request(ICodeSenderData data, Action<string> callback)
        {
            callback?.Invoke("{\"error\":\"Это режим 'без сервера', чтобы тесировать задачи - подключите сервер и уберите директиву SERVERLESS при сборке проекта\"}");
        }

        public async UniTask<string> RequestAsync(ICodeSenderData data, IProgress<float> progress = null)
        {
            return await UniTask.FromResult(
                "{\"error\":\"Это режим 'без сервера', чтобы тесировать задачи - подключите сервер и уберите директиву SERVERLESS при сборке проекта\"}");
        }
    }
}