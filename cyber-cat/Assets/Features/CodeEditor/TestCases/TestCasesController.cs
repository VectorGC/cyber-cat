using Shared.Models.Ids;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TestCasesController : UIBehaviour
{
    [SerializeField] private TestCasesToggleGroup _testCases;
    [SerializeField] private TestCaseDescriptionView _descriptionView;

    private ICodeEditor _codeEditor;

    [Inject]
    private void Construct(ICodeEditor codeEditor)
    {
        _codeEditor = codeEditor;
    }

    protected override void OnEnable()
    {
        _testCases.Switched += OnSwitched;
    }

    protected override void OnDisable()
    {
        _testCases.Switched -= OnSwitched;
    }

    private void OnSwitched(object sender, TestCaseId testCaseId)
    {
        _descriptionView.SetTestCaseAsync(testCaseId).Forget();
    }
}