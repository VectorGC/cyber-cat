using UniMob;
using UnityEngine;

public class TestCasesView : LifetimeUIBehaviour<ConsoleState>
{
    [SerializeField] private TestCasesToggleGroup _testCasesToggleGroup;
    [SerializeField] private TestCaseDescriptionView _descriptionView;

    [Atom] public override ConsoleState State { get; set; }

    protected override void OnInitState(ConsoleState state)
    {
        _testCasesToggleGroup.State = State;
    }

    protected override void OnUpdate()
    {
        if (State?.SelectedTestCaseId != null && State.TestCasesVerdict != null)
            _descriptionView.SetTestCase(State.TestCasesVerdict[State.SelectedTestCaseId]);
    }
}