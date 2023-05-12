using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MainMenu : UIBehaviour
    {
        [SerializeField] private TextMeshPro_TextShared _greetingsText;

        protected override void Start()
        {
            var playerName = PlayerPrefs.GetString("player_name");
            _greetingsText.text = $"Доступ получен: <color=green>{playerName}";
        }
    }
}