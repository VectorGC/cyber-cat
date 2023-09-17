using ApiGateway.Client;
using Features.ServerConfig;
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

        ServerAPI.ServerEnvironment = (ServerEnvironment) EditorGUILayout.EnumPopup(ServerAPI.ServerEnvironment);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}