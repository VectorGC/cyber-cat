using UniMob;
using UnityEngine;

public class ConsoleContentView : LifetimeUIBehaviour<ConsoleState>
{
    [SerializeField] private TestCasesView _testCasesView;
    [Atom] public override ConsoleState State { get; set; }

    protected override void OnInitState(ConsoleState state)
    {
        _testCasesView.State = state;
    }
}