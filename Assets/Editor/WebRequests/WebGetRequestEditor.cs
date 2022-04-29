using UnityEditor;
using UnityEngine;
using WebRequests;

namespace Editor.WebRequests
{
    [CustomEditor(typeof(SendWebRequestBehaviour))]
    public class WebGetRequestEditor : UnityEditor.Editor
    {
        private SendWebRequestBehaviour Target => (SendWebRequestBehaviour) target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Send Get Request"))
            {
                Target.SendGetRequest();
            }
        }
    }
}