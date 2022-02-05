using UnityEditor;
using UnityEngine;

namespace Editor.StreamingVideo
{
    [CustomEditor(typeof(global::StreamingVideo.StreamingVideo))]
    public class StreamingVideoEditor : UnityEditor.Editor
    {
        private SerializedProperty _filePath;
        private SerializedProperty _streamingAsset;

        void OnEnable()
        {
            _filePath = serializedObject.FindProperty(global::StreamingVideo.StreamingVideo.FilePathFieldName);
            _streamingAsset = serializedObject.FindProperty(global::StreamingVideo.StreamingVideo.StreamingVideoAssetFieldName);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_streamingAsset);
            EditorGUILayout.PropertyField(_filePath);

            if (_streamingAsset.objectReferenceValue == null)
            {
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(_streamingAsset.objectReferenceValue.GetInstanceID());
            if (assetPath.StartsWith(Application.streamingAssetsPath))
            {
                assetPath = assetPath.Substring(Application.streamingAssetsPath.Length);
            }

            _filePath.stringValue = assetPath;
            serializedObject.ApplyModifiedProperties();
        }
    }
}