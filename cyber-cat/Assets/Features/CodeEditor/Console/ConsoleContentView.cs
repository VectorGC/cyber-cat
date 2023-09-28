using UniMob;
using UnityEngine;

public class ConsoleContentView : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private TestCasesView _testCasesView;
    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnInitState(CodeEditorState state)
    {
        _testCasesView.State = state;
    }
}