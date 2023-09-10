using System.Text;
using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("UI/", "Log")]
public class Modal : Task
{
    [SerializeField] private string _header = "Info";
    [SerializeField] [Multiline(10)] private string _body = "Text";
    [SerializeField] private string _button = "OK";

    private SimpleModalWindow _visibleWindow;

    public override void OnEnter()
    {
        _visibleWindow = SimpleModalWindow.Create()
            .SetHeader(_header)
            .SetBody(_body)
            .AddButton(_button)
            .Show();
    }

    public override Status Run()
    {
        return _visibleWindow ? Status.Running : Status.Success;
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