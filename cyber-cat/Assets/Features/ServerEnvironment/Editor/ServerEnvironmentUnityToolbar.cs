using ServerAPI;
using UnityEditor;
using UnityEngine;
using UnityToolbarExtender;

[InitializeOnLoad]
public static class ServerEnvironmentUnityToolbar
{
    static ServerEnvironmentUnityToolbar()
    {
        ToolbarExtender.RightToolbarGUI.Remove(OnToolbarGUI);
        ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
    }

    private static void OnToolbarGUI()
    {
        EditorGUI.BeginDisabledGroup(Application.isPlaying);

        ServerEnvironment.Current = (ServerEnvironment.Types) EditorGUILayout.EnumPopup(ServerEnvironment.Current);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}