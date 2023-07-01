using Models;

public interface ICodeEditorView
{
    LanguageProg Language { set; }
    string SourceCode { get; set; }
}