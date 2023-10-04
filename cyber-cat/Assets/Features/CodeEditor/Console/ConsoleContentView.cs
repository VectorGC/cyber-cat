using Shared.Models.Models.Verdicts;
using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleContentView : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private Text _answerSummary;
    [SerializeField] private TestCasesView _testCasesView;
    [SerializeField] private Text _emptyLabel;
    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnInitState(CodeEditorState state)
    {
        _testCasesView.State = state;
    }

    protected override void OnUpdate()
    {
        if (State?.Section is ResultSection {Verdict: null})
        {
            _emptyLabel.text = "Сначала запустите ваш код";
            _emptyLabel.gameObject.SetActive(true);
        }
        else
        {
            _emptyLabel.gameObject.SetActive(false);
        }

        if (State?.Section is ResultSection resultSection)
        {
            switch (resultSection.SelectedTestCaseVerdict)
            {
                case SuccessTestCaseVerdict success:
                    _answerSummary.text = "Верный ответ";
                    _answerSummary.color = Color.green;
                    _answerSummary.gameObject.SetActive(true);
                    break;
                case FailureTestCaseVerdict failure:
                    _answerSummary.text = "Неверный ответ";
                    _answerSummary.color = Color.red;
                    _answerSummary.gameObject.SetActive(true);
                    break;
                default:
                    _answerSummary.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            _answerSummary.gameObject.SetActive(false);
        }
    }
}