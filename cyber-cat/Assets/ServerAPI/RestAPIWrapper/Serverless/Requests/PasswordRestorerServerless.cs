using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;

namespace RestAPIWrapper.Serverless
{
    public class PasswordRestorerServerless : IPasswordRestorer<string>
    {
        public void Request(IPasswordRestorerData data, Action<string> callback)
        {
            const string messageText =
                "��� ����� '��� �������', ����� ������������ �������� ���������� ������������ ���� - ���������� �������� ������";
            callback?.Invoke(messageText);
        }

        public async UniTask<string> RequestAsync(IPasswordRestorerData data, IProgress<float> progress = null)
        {
            return await UniTask.FromResult(string.Empty);
        }
    }
}