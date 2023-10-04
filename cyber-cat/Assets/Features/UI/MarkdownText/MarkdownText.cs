using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LogicUI.FancyTextRendering;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(MarkdownRenderer))]
public class MarkdownText : UIBehaviour, IText
{
    private MarkdownRenderer _markdownRenderer;

    public string Text
    {
        get => _markdownRenderer.Source;
        set => _markdownRenderer.Source = value;
    }

    private void Awake()
    {
        TryGetComponent(out _markdownRenderer);
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