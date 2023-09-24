using Cysharp.Threading.Tasks;
using UniMob;
using UnityEngine;
using Zenject;

public class ConsoleContentView : LifetimeMonoBehaviour
{
    [SerializeField] private TestCasesView _testCasesView;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override async void Start()
    {
        var state = new ConsoleState(Lifetime);

        _testCasesView.State = state;

        await UniTask.WaitUntil(() => _codeEditor.Task != null);

        /*
        var verdict = await _codeEditor.Task.VerifySolutionV2("Test");
        state.Section = new ResultSection(Lifetime)
        {
            Verdict = verdict
        };
        */

        var testCases = await _codeEditor.Task.GetTestCases();
        state.Section = new TestCasesSection(Lifetime)
        {
            TestCases = testCases
        };
    }
}