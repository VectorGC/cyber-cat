using UnityEngine;
using UnityEngine.UI;

public class LoadWindowView : MonoBehaviour, ILoadWindow
{
    [SerializeField] private Image _imageLogo;
    [SerializeField] private float _timeScale;
    
    private static LoadWindowView _instance;
    public static LoadWindowView Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Instantiate(UIDialogs.Instance.LoadWindowPrefab, FindObjectOfType<Canvas>().transform, false);
                _instance.transform.SetAsLastSibling();
                _instance.gameObject.SetActive(false);
            }

            return _instance;
        }
    }
    
    private float _stepOfAlphaChange;
    private float _targetAlpha;
    
    private Color _color;

    private void Start()
    {
        _color = _imageLogo.color;
        _targetAlpha = 1;
    }

    private void Update()
    {
        if (_color.a < 0.01 || _color.a > 0.99)
        {
            _stepOfAlphaChange = 0;
            _targetAlpha = 1 - _targetAlpha;
        }
        _stepOfAlphaChange += Time.deltaTime * _timeScale;
        _color.a = Mathf.Lerp(_color.a, _targetAlpha, Easing(_stepOfAlphaChange));
        _imageLogo.color = _color;
    }

    private float Easing(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    public void StartLoading()
    {
        gameObject.SetActive(true);
    }

    public void StopLoading()
    {
        gameObject.SetActive(false);
    }
}

