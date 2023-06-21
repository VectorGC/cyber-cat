using System;
using Cysharp.Threading.Tasks;

public interface ICodeEditorService
{
    UniTaskVoid OpenEditor(string taskId, IProgress<float> progress = null);
    UniTaskVoid CloseEditor(IProgress<float> progress = null);
}