using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public float letterSpeed = 1f; // �������� �������� ����
    public TMP_Text textMeshPro;

    private void Start()
    {
        // ��������� �������� ��� �������� ������
        StartCoroutine(AnimateText());
    }

    private System.Collections.IEnumerator AnimateText()
    {
        while (true)
        {
            // �������� ������� �����
            string originalText = textMeshPro.text;

            // ������� ����� ������ ��� �������������� ������
            string animatedText = "";

            // ���������� ������� � ������������ ������
            for (int i = 0; i < originalText.Length; i++)
            {
                // �������� ������� ������
                char character = originalText[i];

                // ��������� ������ � ������������� ����� � ��������� ��������� �� ��� Y
                animatedText += "<voffset=" + Random.Range(-0.5f, 0.5f) + ">" + character + "</voffset>";

                // ���� ��������� ����� ����� ����������� ���������� �������
                yield return new WaitForSeconds(letterSpeed);
            }

            // ����������� ������������� ����� ���������� TextMeshPro
            textMeshPro.text = animatedText;
        }
    }
}
