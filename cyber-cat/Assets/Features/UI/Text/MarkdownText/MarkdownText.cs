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

    public Color Color
    {
        get => _markdownRenderer.TextMesh.color;
        set => _markdownRenderer.TextMesh.color = value;
    }

    protected override void Awake()
    {
        TryGetComponent(out _markdownRenderer);
    }

    public void SetText(string text)
    {
        Text = text;
    }
}