using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    public enum Type
    {
        Message,
        Event,
        MessageAndEvent
    }

    public Type TypeOfTrigger;

    private SerializedProperty countOfDialogs;
    private int count;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DisplayCommonInfo();

        switch (TypeOfTrigger)
        {
            case Type.Message:
                DisplayMessagesInfo();
                break;

            case Type.Event:
                DisplayEvent();
                break;

            case Type.MessageAndEvent:
                DisplayMessagesInfo();
                DisplayEvent();
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }


    private void DisplayCommonInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_transformOfTrigger"));
        EditorGUILayout.Space();
        TypeOfTrigger = (Type)EditorGUILayout.EnumPopup("Type", TypeOfTrigger);
        EditorGUILayout.Space();
    }

    private void DisplayMessagesInfo()
    {
        countOfDialogs = serializedObject.FindProperty("_countOfModals");
        //count = countOfDialogs.intValue;
        //count = EditorGUILayout.IntSlider("Count of messages", count, 0, 10);
        countOfDialogs.intValue = EditorGUILayout.IntField("Count", countOfDialogs.intValue);
        if (countOfDialogs.intValue > 0)
        {
            serializedObject.FindProperty("_modalInfos").arraySize = countOfDialogs.intValue;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_modalInfos"));
        }
        EditorGUILayout.Space();
    }

    private void DisplayEvent()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_onEnter"));
        EditorGUILayout.Space();
    }
}
