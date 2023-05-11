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

        RestAPIFacade.ServerEnvironment = (ServerEnvironment) EditorGUILayout.EnumPopup(RestAPIFacade.ServerEnvironment);

        EditorGUI.EndDisabledGroup();

        GUILayout.FlexibleSpace();
    }
}