using ApiGateway.Client.Models;
using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class TestCaseToggle : LifetimeUIBehaviour<CodeEditorState>
{
    [field: SerializeField] public Toggle Toggle { get; private set; }
    [SerializeField] private Image _statusIcon;
    [SerializeField] private Text _name;
    [SerializeField] private Sprite _successIcon;
    [SerializeField] private Sprite _failureIcon;

    [Atom] public override CodeEditorState State { get; set; }

    private int _index;

    protected override void OnInitView()
    {
        Toggle.onValueChanged.AddListener(OnValueChanged);
    }

    protected override void OnDisposeView()
    {
        Toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    public void SetIndexNumber(int index)
    {
        _index = index;
    }

    protected override void OnInitState(CodeEditorState state)
    {
        if (Toggle.isOn)
        {
            OnValueChanged(true); // Revoke event
        }
    }

    protected override void OnUpdate()
    {
        var testCaseId = State?.GetTestCaseIdAtIndex(_index);
        if (testCaseId == null)
        {
            gameObject.SetActive(false);
            return;
        }

        var testCaseVerdict = State.GetTestCaseVerdictById(testCaseId);
        if (testCaseVerdict != null)
        {
            _name.text = $"Тест {testCaseId}";
            _statusIcon.sprite = testCaseVerdict is SuccessTestCaseVerdict ? _successIcon : _failureIcon;
            _statusIcon.color = testCaseVerdict is SuccessTestCaseVerdict ? Color.green : Color.red;
            _statusIcon.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
        else if (State.GetTestCaseById(testCaseId) != null)
        {
            _name.text = $"Тест {testCaseId}";
            _statusIcon.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnValueChanged(bool value)
    {
        if (value && State != null)
        {
            State.SelectTestCaseAtIndex(_index);
        }
    }
}