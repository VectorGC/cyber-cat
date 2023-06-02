using System;
using Cysharp.Threading.Tasks;
using Models;

public interface ICodeEditorService
{
    ITask CurrentTask { get; }
    UniTaskVoid OpenEditor(ITask task, IProgress<float> progress = null);
    UniTaskVoid CloseEditor(IProgress<float> progress = null);
    UniTask<string> LoadSavedCode(ITask task, IProgress<float> progress = null);
    UniTask<IVerdict> VerifySolution(ITask task, string sourceCode, IProgress<float> progress = null);
}