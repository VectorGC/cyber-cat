using System.Collections.Generic;
using System.Linq;
using ApiGateway.Client.Models;
using Shared.Models.Ids;
using UniMob;

public class ConsoleState : ILifetimeScope
{
    [Atom] public ISection Section { get; set; }

    public Lifetime Lifetime { get; }

    public ConsoleState(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }

    public void SelectTestCaseAtIndex(int index)
    {
        switch (Section)
        {
            case ResultSection resultSection:
                resultSection.SelectedTestCaseId = resultSection.TestCaseIds[index];
                break;
            case TestCasesSection testCasesSection:
                testCasesSection.SelectedTestCaseId = testCasesSection.TestCaseIds[index];
                break;
        }
    }

    public TestCaseId GetTestCaseIdAtIndex(int index)
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.TestCaseIds[index],
            TestCasesSection testCasesSection => testCasesSection.TestCaseIds[index],
            _ => null
        };
    }

    public ITestCaseVerdict GetTestCaseVerdictById(TestCaseId testCaseId)
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.TestCasesVerdict[testCaseId],
            _ => null
        };
    }

    public ITestCase GetTestCaseById(TestCaseId testCaseId)
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.TestCases[testCaseId],
            _ => null
        };
    }

    public ITestCaseVerdict GetSelectedTestCaseVerdict()
    {
        return Section switch
        {
            ResultSection resultSection => resultSection.SelectedTestCaseVerdict,
            _ => null
        };
    }

    public ITestCase GetSelectedTestCase()
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.SelectedTestCase,
            _ => null
        };
    }

    public TestCaseId GetSelectedTestCaseId()
    {
        return Section switch
        {
            TestCasesSection testCasesSection => testCasesSection.SelectedTestCaseId,
            ResultSection resultSection => resultSection.SelectedTestCaseId,
            _ => null
        };
    }
}

public interface ISection : ILifetimeScope
{
}

public class TestCasesSection : ISection
{
    [Atom] public TestCases TestCases { get; set; }
    [Atom] public TestCaseId SelectedTestCaseId { get; set; }
    [Atom] public List<TestCaseId> TestCaseIds => TestCases?.Ids.ToList();
    [Atom] public ITestCase SelectedTestCase => SelectedTestCaseId != null ? TestCases?[SelectedTestCaseId] : null;

    public Lifetime Lifetime { get; }

    public TestCasesSection(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}

public class ResultSection : ISection
{
    [Atom] public IVerdictV2 Verdict { get; set; }
    [Atom] public TestCaseId SelectedTestCaseId { get; set; }
    [Atom] public TestCasesVerdict TestCasesVerdict => Verdict as TestCasesVerdict;
    [Atom] public List<TestCaseId> TestCaseIds => TestCasesVerdict?.Ids.ToList();
    [Atom] public ITestCaseVerdict SelectedTestCaseVerdict => SelectedTestCaseId != null ? TestCasesVerdict?[SelectedTestCaseId] : null;

    public Lifetime Lifetime { get; }

    public ResultSection(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}