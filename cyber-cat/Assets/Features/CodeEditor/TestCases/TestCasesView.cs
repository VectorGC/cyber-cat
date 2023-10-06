using UniMob;
using UnityEngine;

public class TestCasesView : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private TestCasesToggleGroup _testCasesToggleGroup;
    [SerializeField] private TestCaseDescriptionView _descriptionView;

    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnInitState(CodeEditorState state)
    {
        _testCasesToggleGroup.State = State;
        _descriptionView.State = State;
    }
}