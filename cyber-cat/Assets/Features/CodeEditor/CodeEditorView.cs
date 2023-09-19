using Models;
using UnityEngine.EventSystems;

public abstract class CodeEditorView : UIBehaviour
{
    public abstract LanguageProg Language { get; set; }
    public abstract string SourceCode { get; set; }
}