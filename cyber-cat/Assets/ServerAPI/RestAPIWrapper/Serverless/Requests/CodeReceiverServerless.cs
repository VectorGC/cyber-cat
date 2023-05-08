using Cysharp.Threading.Tasks;
using ServerAPIBase;
using System;
using UniRx;

namespace RestAPIWrapper.Serverless
{
    public class CodeReceiverServerless : ICodeReceiver<string>
    {
        public void Request(ICodeReceiverData data, Action<string> callback)
        {
            const string messageText =
                "��� ����� '��� �������', ����� ������������ �������� ���������� ������������ ���� - ���������� �������� ������";
            callback?.Invoke(messageText);
        }

        public async UniTask<string> RequestAsync(ICodeReceiverData data, IProgress<float> progress = null)
        {
            return await UniTask.FromResult(string.Empty);
        }
    }
}