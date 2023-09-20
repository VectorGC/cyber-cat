using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LogicUI.FancyTextRendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkdownText : UIBehaviour, IText
{
    [SerializeField] private MarkdownRenderer _markdownRenderer;

    public string Text
    {
        get => _markdownRenderer.Source;
        set => _markdownRenderer.Source = value;
    }

    public void SetTextAsync(Task<string> handler)
    {
        SetTextAsync(handler.AsUniTask()).Forget();
    }

    private async UniTaskVoid SetTextAsync(UniTask<string> handler)
    {
        Text = await handler;
    }
}