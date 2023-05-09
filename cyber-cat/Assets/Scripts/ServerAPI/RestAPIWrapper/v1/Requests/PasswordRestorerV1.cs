using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.V1
{
    public class PasswordRestorerV1 : IPasswordRestorer<string>
    {
        public void Request(IPasswordRestorerData data, Action<string> callback)
        {
            throw new NotImplementedException();
        }

        public UniTask<string> RequestAsync(IPasswordRestorerData data, IProgress<float> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}