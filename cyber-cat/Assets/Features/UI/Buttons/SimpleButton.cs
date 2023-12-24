using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SimpleButton : UIBehaviour, IButton
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _label;
    [SerializeField] private Image _highlight;
    [SerializeField] [Range(0, 5)] private int _highlightSpeed = 2;

    public event Action OnClick;

    public string Text
    {
        get => _label.text;
        set => _label.text = value;
    }

    private Coroutine _highlightCoroutine;

    protected override void Start()
    {
        _button.onClick.AddListener(OnClickHandler);
    }

    protected override void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClickHandler);

        StopHighlightCoroutine();
    }

    private void OnClickHandler()
    {
        OnClick?.Invoke();
    }


#if UNITY_EDITOR
    protected override void OnValidate()
    {
        TryGetComponent(out _button);
    }
#endif

    public void SetActiveHighlight(bool isHighlight)
    {
        if (isHighlight && _highlightCoroutine == null)
        {
            _highlightCoroutine = StartCoroutine(HighlightCoroutine());
        }

        if (!isHighlight && _highlightCoroutine != null)
        {
            StopHighlightCoroutine();
        }
    }

    private void StopHighlightCoroutine()
    {
        if (_highlightCoroutine != null)
        {
            StopCoroutine(_highlightCoroutine);
            _highlightCoroutine = null;
        }
    }

    private IEnumerator HighlightCoroutine()
    {
        var time = 0f;
        var greatAlpha = true;
        while (true)
        {
            var color = _highlight.color;
            color.a = Mathf.Lerp(0, 1, time);
            _highlight.color = color;

            if (greatAlpha)
                time += _highlightSpeed * Time.deltaTime;
            else
                time -= _highlightSpeed * Time.deltaTime;

            if (time >= 1)
                greatAlpha = false;
            if (time <= 0)
                greatAlpha = true;

            yield return new WaitForEndOfFrame();
        }
    }

    public void UnsubscribeAll()
    {
        OnClick = null;
    }
}