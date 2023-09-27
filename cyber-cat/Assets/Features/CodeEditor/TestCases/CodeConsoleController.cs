using UniMob;
using UnityEngine;

public class CodeConsoleController : LifetimeMonoBehaviour
{
    [SerializeField] private ConsoleToolSectionToggleGroup _sectionsToggleGroup;
    [SerializeField] private ConsoleContentView _consoleContentView;

    [Atom] public ConsoleState State { get; set; }

    protected override void Start()
    {
        base.Start();

        State = new ConsoleState(Lifetime);
        _sectionsToggleGroup.State = State;
        _consoleContentView.State = State;
    }
}