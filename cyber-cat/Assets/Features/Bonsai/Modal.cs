using Bonsai;
using Bonsai.Core;
using UnityEngine;

[BonsaiNode("UI/", "Modal")]
public class Modal : Task
{
    [SerializeField] [Multiline]
    public string _body = "Text";
    
    private SimpleModalWindow _visibleWindow;

    public override void OnEnter()
    {
        _visibleWindow = SimpleModalWindow.Create()
            .SetBody(_body)
            .Show();
    }

    public override Status Run()
    {
        return _visibleWindow ? Status.Running : Status.Success;
    }
}