using System.Collections.Generic;
using Shared.Models.Ids;
using UniMob;
using UnityEngine.UI;

public class TestCasesToggleGroup : ToggleGroup, ILifetimeScope
{
    [Atom] public TestCaseId Selected { get; private set; }
    [Atom] public List<TestCaseId> TestCaseIds { get; set; }

    public Lifetime Lifetime => _lifetimeController.Lifetime;
    private readonly LifetimeController _lifetimeController = new LifetimeController();

    private TestCaseToggle[] _toggles;

    protected override void Awake()
    {
        _toggles = GetComponentsInChildren<TestCaseToggle>();
        foreach (var toggle in _toggles)
        {
            RegisterToggle(toggle.Toggle);
            toggle.Toggle.group = this;
            toggle.Enabled += OnEnabled;
        }
    }

    protected override void Start()
    {
        base.Start();
        Atom.Reaction(Lifetime, OnUpdate, debugName: nameof(TestCasesToggleGroup));
    }

    protected override void OnDestroy()
    {
        foreach (var toggle in _toggles)
        {
            UnregisterToggle(toggle.Toggle);
            toggle.Toggle.group = null;
            toggle.Enabled -= OnEnabled;
        }
    }

    private void OnUpdate()
    {
        if (TestCaseIds == null)
        {
            return;
        }

        var i = 0;
        for (; i < TestCaseIds.Count; i++)
        {
            _toggles[i].TestCaseId = TestCaseIds[i];
        }

        for (; i < _toggles.Length; i++)
        {
            _toggles[i].gameObject.SetActive(false);
        }

        EnsureValidState();

        // Revoke update.
        GetFirstActiveToggle().isOn = false;
        GetFirstActiveToggle().isOn = true;
    }

    private void OnEnabled(object sender, TestCaseId id)
    {
        Selected = id;
    }
}