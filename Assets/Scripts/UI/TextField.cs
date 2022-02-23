using TMPro;
using UnityEngine;

public class TextField : MonoBehaviour
{
    [SerializeField] private TMP_Text labelContent;
    [SerializeField] private TMP_Text textContent;

    public void SetContent(string text, string label)
    {
        SetContent(text);

        labelContent.text = label;
        labelContent.gameObject.SetActive(true);
    }

    public void SetContent(string text)
    {
        textContent.text = text;

        labelContent.gameObject.SetActive(false);
        textContent.gameObject.SetActive(true);
    }
}