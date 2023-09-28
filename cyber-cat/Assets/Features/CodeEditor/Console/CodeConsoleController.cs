using UniMob;
using UnityEngine;

public class CodeConsoleController : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private ConsoleToolSectionToggleGroup _sectionsToggleGroup;
    [SerializeField] private ConsoleContentView _consoleContentView;

    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnInitState(CodeEditorState state)
    {
        _sectionsToggleGroup.State = state;
        _consoleContentView.State = state;
    }
}