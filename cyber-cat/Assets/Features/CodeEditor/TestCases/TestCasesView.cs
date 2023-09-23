using System.Linq;
using ApiGateway.Client.Models;
using UniMob;
using UnityEngine;

public class TestCasesView : LifetimeUIBehaviour, ITestCasesView
{
    [SerializeField] private TestCasesToggleGroup _testCasesToggleGroup;
    [SerializeField] private TestCaseDescriptionView _descriptionView;

    [Atom] public TestCasesVerdict TestCasesVerdict { get; set; }

    public TestCases TestCases { get; set; }

    protected override void OnUpdate()
    {
        if (TestCasesVerdict != null)
        {
            _testCasesToggleGroup.TestCaseIds = TestCasesVerdict.Ids.ToList();
            if (_testCasesToggleGroup.Selected != null)
            {
                _descriptionView.SetTestCase(TestCasesVerdict[_testCasesToggleGroup.Selected]);
            }
        }
    }
}