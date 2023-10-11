using UnityEditor;
using UnityEngine;

namespace Assets.SimpleVKSignIn.Scripts.Editor
{
    [CustomEditor(typeof(Settings))]
    public class SettingsEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("- Get [App ID] and [Secure key] on VK Developers portal.\n- Custom URI Scheme (Protocol) should be unique to your app.", MessageType.None);

            DrawDefaultInspector();

            var settings = (Settings) target;
            
            if (!settings.Redefined())
            {
                EditorGUILayout.HelpBox("Test settings are in use. They are for test purposes only and may be disabled or blocked. Please set your own settings obtained from VK Developers.", MessageType.Warning);
            }

            if (GUILayout.Button("VK Developers"))
            {
                Application.OpenURL("https://dev.vk.com/ru");
            }

            if (GUILayout.Button("Wiki"))
            {
                Application.OpenURL("https://github.com/hippogamesunity/SimpleVKSignIn/wiki");
            }
        }
    }
}