using JetBrains.Annotations;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;

[UsedImplicitly]
[Action("UI/ShowSimpleModalWindow")]
public class ShowSimpleModalWindow : BasePrimitiveAction
{
    private SimpleModalWindow _visibleWindow;

    [InParam("Header", DefaultValue = "Header")]
    public string Header { get; set; }

    [InParam("Text", DefaultValue = "Text")]
    public string Text { get; set; }

    [InParam("ButtonText", DefaultValue = "OK")]
    public string ButtonText { get; set; }

    public override void OnStart()
    {
        _visibleWindow = SimpleModalWindow.Create()
            .SetHeader(Header)
            .SetBody(Text)
            .AddButton(ButtonText)
            .Show();
    }

    public override TaskStatus OnUpdate()
    {
        if (_visibleWindow)
        {
            return TaskStatus.RUNNING;
        }

        return TaskStatus.COMPLETED;
    }
}