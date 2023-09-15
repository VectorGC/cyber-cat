using System;
using System.Text;
using Bonsai;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[BonsaiNode("UI/", "Log")]
public class Modal : AsyncTask
{
    [SerializeField] private string _header = "Info";
    [SerializeField] [Multiline(10)] private string _body = "Text";
    [SerializeField] private string _button = "OK";

    private IModal _visibleWindow;
    private IModalFactory _modalFactory;

    [Inject]
    public void Construct(IModalFactory modalFactory)
    {
        _modalFactory = modalFactory;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _visibleWindow = _modalFactory.Ok(_header, _body, _button);
        _visibleWindow.Show();
    }

    protected override async UniTask<Status> RunAsync()
    {
        if (!_visibleWindow.IsShow)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            return Status.Success;
        }

        return Status.Running;
    }

    public override void Description(StringBuilder builder)
    {
        base.Description(builder);

        builder.AppendLine(_header);
        if (!string.IsNullOrEmpty(_body))
        {
            var text = _body.Length > 20 ? new string(_body.ToCharArray(0, 20)) + "..." : _body;
            builder.AppendLine(text);
        }

        builder.AppendLine("---------");
        builder.AppendLine(_button);
    }
}