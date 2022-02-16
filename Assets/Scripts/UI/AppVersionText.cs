using TMPro;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(TMP_Text))]
public class AppVersionText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        TryGetComponent(out text);
        text.text = $"v {Application.version}";
    }
}