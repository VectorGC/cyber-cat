using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleContentView : LifetimeUIBehaviour<CodeEditorState>
{
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
    }
}