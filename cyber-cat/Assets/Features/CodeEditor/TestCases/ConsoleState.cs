using System.Collections.Generic;
using System.Linq;
using ApiGateway.Client.Models;
using Shared.Models.Ids;
using UniMob;

public class ConsoleState : ILifetimeScope
{
    [Atom] public IVerdictV2 Verdict { get; set; }
    [Atom] public TestCasesVerdict TestCasesVerdict => Verdict as TestCasesVerdict;
    [Atom] public List<TestCaseId> TestCaseIds => TestCasesVerdict?.Ids.ToList();
    [Atom] public TestCaseId SelectedTestCaseId { get; set; }

    public Lifetime Lifetime { get; }

    public ConsoleState(Lifetime lifetime)
    {
        Lifetime = lifetime;
    }
}