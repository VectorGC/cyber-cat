using TMPro;
using UnityEngine;

public class BottomTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text _bottomText;
    
    public void Show()
    {
        _bottomText.gameObject.SetActive(true);
    }

    public void UnShow()
    {
        _bottomText.gameObject.SetActive(false);
    }
}
