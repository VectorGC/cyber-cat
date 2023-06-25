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

        ServerAPIFacade.ServerEnvironment = (ServerEnvironment) EditorGUILayout.EnumPopup(ServerAPIFacade.ServerEnvironment);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}