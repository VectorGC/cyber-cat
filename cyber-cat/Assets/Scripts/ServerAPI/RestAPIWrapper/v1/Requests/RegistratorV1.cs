using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class RegistratorV1 : IRegistrator<string>
    {
        public void Request(IRegistratorData data, Action<string> callback)
        {
            throw new NotImplementedException();
        }

        public async UniTask<string> RequestAsync(IRegistratorData data, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}