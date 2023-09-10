using Bonsai;
using Bonsai.Core;

[BonsaiNode("Conditional/")]
public class IsCloseCodeEditor : ConditionalAbort
{
    public override bool Condition()
    {
        return !CodeEditor.IsOpen;
    }
}