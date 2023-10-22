using UnityEngine;

namespace Assets.SimpleVKSignIn.Scripts
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Simple VK Sign-In/Settings")]
    public class Settings : ScriptableObject
    {
        [SerializeField] private string _appId;
        [SerializeField] private string _secureKey;
        public string CustomUriScheme;

        public string ClientId => _appId;
        public string ClientSecret => _secureKey;

        private static Settings _instance;

        public static Settings Instance => _instance ??= Resources.Load<Settings>("Settings");

        public bool Redefined()
        {
            return ClientId != "51732203"
                   && ClientSecret != "j3lvv5oL98V6qjaKGpPp"
                   && CustomUriScheme != "simple.oauth";
        }
    }
}