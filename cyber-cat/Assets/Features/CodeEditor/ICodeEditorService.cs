using System;
using Cysharp.Threading.Tasks;
using Shared.Models;

public interface ICodeEditorService
{
    UniTaskVoid OpenEditor(string taskId, IProgress<float> progress = null);
    UniTaskVoid CloseEditor(IProgress<float> progress = null);
}