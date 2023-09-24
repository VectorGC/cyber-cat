using System;
using Shared.Models.Ids;
using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class TestCaseToggle : LifetimeUIBehaviour<ConsoleState>
{
    [field: SerializeField] public Toggle Toggle { get; private set; }
    [SerializeField] private Image _statusIcon;
    [SerializeField] private Text _name;
    [SerializeField] private Sprite _successIcon;
    [SerializeField] private Sprite _failureIcon;
    [SerializeField] private Color _success;
    [SerializeField] private Color _failure;

    [Atom] public override ConsoleState State { get; set; }

    private TestCaseId _testCaseId;
    private int _index;

    protected void OnEnable()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    protected void OnDisable()
    {
        Toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void SetIndexNumber(int index)
    {
        _index = index;
    }

    protected override void OnInitState(ConsoleState state)
    {
        if (Toggle.isOn && State != null)
        {
            var testCaseId = State.TestCaseIds[_index];
            State.SelectedTestCaseId = testCaseId;
        }
    }

    protected override void OnUpdate()
    {
        if (State?.TestCaseIds != null)
        {
            if (State.TestCaseIds.Count > _index)
            {
                _testCaseId = State.TestCaseIds[_index];
                _name.text = $"Тест {_testCaseId}";
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnValueChanged(bool value)
    {
        if (value && State != null)
        {
            var testCaseId = State.TestCaseIds[_index];
            State.SelectedTestCaseId = testCaseId;
        }
    }
}