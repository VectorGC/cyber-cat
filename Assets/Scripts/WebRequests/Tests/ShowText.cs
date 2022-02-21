using UnityEngine;
using UnityEngine.UI;

namespace Tests
{
    public class ShowText : MonoBehaviour
    {
        public Text text;

        public void Show()
        {
            Show("Кирилл, привет)");
        }

        public void Show(string message)
        {
            text.gameObject.SetActive(true);
            text.text = message;
        }
    }
}
