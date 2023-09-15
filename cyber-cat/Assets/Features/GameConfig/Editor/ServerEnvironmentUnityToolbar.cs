using ApiGateway.Client;
using Features.GameManager;
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

        GameConfig.ServerEnvironment = (ServerEnvironment) EditorGUILayout.EnumPopup(GameConfig.ServerEnvironment);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}