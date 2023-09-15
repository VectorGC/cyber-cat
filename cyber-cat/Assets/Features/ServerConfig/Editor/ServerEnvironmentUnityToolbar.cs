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

        GameManager.ServerEnvironment = (ServerEnvironment) EditorGUILayout.EnumPopup(GameManager.ServerEnvironment);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}