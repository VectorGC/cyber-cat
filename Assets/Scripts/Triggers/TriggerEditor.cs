using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : Editor
{
    [SerializeField] private Trigger.EventType TypeOfEvent;
    [SerializeField] private Trigger.TriggerType TypeOfTrigger;

    [SerializeField] private SerializedProperty countOfDialogs;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DisplayCommonInfo();

        switch (TypeOfEvent)
        {
            case Trigger.EventType.Message:
                DisplayMessagesInfo();
                break;

            case Trigger.EventType.EnterEvent:
                DisplayEvent();
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }


    private void DisplayCommonInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_triggerType"));
        TypeOfTrigger = (Trigger.TriggerType)serializedObject.FindProperty("_triggerType").enumValueIndex;
        Debug.Log(TypeOfTrigger);
        EditorGUILayout.Space();

        switch (TypeOfTrigger)
        {
            case Trigger.TriggerType.Enter:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_transformOfTrigger"));
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_eventType"));
                TypeOfEvent = (Trigger.EventType)serializedObject.FindProperty("_eventType").enumValueIndex;
                break;

            case Trigger.TriggerType.ButtonPressed:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_keyCodes"));
                break;
        }
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
