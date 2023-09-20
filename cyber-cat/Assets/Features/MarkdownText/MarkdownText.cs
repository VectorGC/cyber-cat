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
}