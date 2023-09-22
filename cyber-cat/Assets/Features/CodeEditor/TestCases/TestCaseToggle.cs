using System;
using Shared.Models.Ids;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestCaseToggle : UIBehaviour
{
    public event EventHandler<TestCaseId> Enabled;

    [field: SerializeField] public Toggle Toggle { get; private set; }
    [SerializeField] private Text _name;

    public TestCaseId TestCaseId
    {
        get => _testCaseId;
        set
        {
            _testCaseId = value;
            _name.text = $"Тест {_testCaseId}";
        }
    }

    private TestCaseId _testCaseId;

    protected override void OnEnable()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void OnDisable()
    {
        Toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        if (value)
        {
            Enabled?.Invoke(this, TestCaseId);
        }
    }
}