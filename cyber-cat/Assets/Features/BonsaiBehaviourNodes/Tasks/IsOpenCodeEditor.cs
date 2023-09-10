using Bonsai;
using Bonsai.Core;

[BonsaiNode("Conditional/")]
public class IsOpenCodeEditor : ConditionalAbort
{
    public override bool Condition()
    {
        return CodeEditor.IsOpen;
    }
}