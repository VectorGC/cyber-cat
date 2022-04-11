using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTextController : MonoBehaviour
{
    [SerializeField] private TextMeshPro_TextShared _bottomText;
    
    public void Show()
    {
        _bottomText.gameObject.SetActive(true);
    }

    public void UnShow()
    {
        _bottomText.gameObject.SetActive(false);
    }
}
