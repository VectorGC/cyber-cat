using System;
using Cysharp.Threading.Tasks;
using Models;

public interface ICodeEditorService
{
    UniTaskVoid OpenEditor(string taskId, IProgress<float> progress = null);
    UniTaskVoid CloseEditor(IProgress<float> progress = null);
    UniTask<string> GetSavedCode();
    UniTask<ITask> GetCurrentTask();
    UniTask<IVerdict> VerifySolution(string sourceCode);
}