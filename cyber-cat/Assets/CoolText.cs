using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public float letterSpeed = 1f; // Скорость анимации букв
    public TMP_Text textMeshPro;

    private void Start()
    {
        // Запускаем корутину для анимации текста
        StartCoroutine(AnimateText());
    }

    private System.Collections.IEnumerator AnimateText()
    {
        while (true)
        {
            // Получаем текущий текст
            string originalText = textMeshPro.text;

            // Создаем новую строку для анимированного текста
            string animatedText = "";

            // Перебираем символы в оригинальном тексте
            for (int i = 0; i < originalText.Length; i++)
            {
                // Получаем текущий символ
                char character = originalText[i];

                // Добавляем символ в анимированный текст с небольшим смещением по оси Y
                animatedText += "<voffset=" + Random.Range(-0.5f, 0.5f) + ">" + character + "</voffset>";

                // Ждем некоторое время перед добавлением следующего символа
                yield return new WaitForSeconds(letterSpeed);
            }

            // Присваиваем анимированный текст компоненту TextMeshPro
            textMeshPro.text = animatedText;
        }
    }
}
