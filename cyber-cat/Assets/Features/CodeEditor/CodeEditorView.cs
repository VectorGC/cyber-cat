using Models;
using UnityEngine.EventSystems;

public abstract class CodeEditorView : UIBehaviour
{
    public LanguageProg Language { get; set; }
    public string SourceCode { get; set; }
}