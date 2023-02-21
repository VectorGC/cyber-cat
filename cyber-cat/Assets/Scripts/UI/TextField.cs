using TMPro;
using UnityEngine;

public class TextField : MonoBehaviour
{
    [SerializeField] private TMP_Text textContent;

    private void Start()
    {
        SetText(string.Empty);
        textContent.gameObject.SetActive(true);
    }

    public void SetText(string text)
    {
        textContent.text = text;
    }
}