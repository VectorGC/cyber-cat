using System.Linq;
using ApiGateway.Client.Models;
using Shared.Models.Ids;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestCasesView : UIBehaviour, ITestCasesView
{
    [SerializeField] private TestCasesToggleGroup _testCasesToggleGroup;
    [SerializeField] private TestCaseDescriptionView _descriptionView;

    private TestCases _testCases;
    private TestCasesVerdict _testCasesVerdict;

    public void Show(TestCases testCases)
    {
        _testCases = testCases;
    }

    public void Show(TestCasesVerdict testCasesVerdict)
    {
        _testCasesVerdict = testCasesVerdict;
        _testCasesToggleGroup.Show(testCasesVerdict.Ids.ToList());
    }

    public void Hide()
    {
        _testCasesToggleGroup.Hide();
    }

    protected override void Awake()
    {
        _testCasesToggleGroup.Switched += OnSwitched;
    }

    protected override void OnDestroy()
    {
        _testCasesToggleGroup.Switched -= OnSwitched;
    }

    private void OnSwitched(object sender, TestCaseId testCaseId)
    {
        if (_testCases != null)
        {
            _descriptionView.SetTestCase(_testCases[testCaseId]);
            return;
        }

        if (_testCasesVerdict != null)
        {
            _descriptionView.SetTestCase(_testCasesVerdict[testCaseId]);
            return;
        }
    }
}