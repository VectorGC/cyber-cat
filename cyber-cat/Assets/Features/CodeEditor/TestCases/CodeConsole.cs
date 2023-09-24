using UniMob;
using UnityEngine;

public class CodeConsole : LifetimeUIBehaviour<ConsoleState>
{
    [SerializeField] private ConsoleToolSectionToggleGroup _sectionsToggleGroup;

    [Atom] public override ConsoleState State { get; set; }

    protected override void OnInitState(ConsoleState state)
    {
        _sectionsToggleGroup.State = state;
    }
}