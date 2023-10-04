using UniMob;
using UnityEngine;
using UnityEngine.UI;

public class CodeEditorStatusBar : LifetimeUIBehaviour<CodeEditorState>
{
    [SerializeField] private GameObject _root;
    [SerializeField] private Text _text;

    [Atom] public override CodeEditorState State { get; set; }

    protected override void OnUpdate()
    {
        if (State?.ProgressStatus != null)
        {
            _root.SetActive(true);
            _text.text = State.ProgressStatus.Value.StatusText;
        }
        else
        {
            _root.SetActive(false);
        }
    }
}