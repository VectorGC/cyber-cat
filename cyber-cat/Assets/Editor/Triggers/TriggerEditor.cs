using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Trigger))]
public class TriggerEditor : UnityEditor.Editor
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

            case Trigger.EventType.CustomScript:
                DisplayEvent();
                break;
        }

        serializedObject.ApplyModifiedProperties();

    }


    private void DisplayCommonInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_canBeActivatedMultipleTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_requiredTriggers"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_banTriggers"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_triggerType"));
        TypeOfTrigger = (Trigger.TriggerType)serializedObject.FindProperty("_triggerType").enumValueIndex;
        EditorGUILayout.Space();

        switch (TypeOfTrigger)
        {
            case Trigger.TriggerType.Enter:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_transformOfTrigger"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onEnter"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onExit"));

                break;

            case Trigger.TriggerType.ButtonPressed:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_keyCodes"));
                break;
            case Trigger.TriggerType.EnterAndPress:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_transformOfTrigger"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onEnter"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_onExit"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("_keyCodes"));
                break;
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_eventType"));
        TypeOfEvent = (Trigger.EventType)serializedObject.FindProperty("_eventType").enumValueIndex;
        EditorGUILayout.Space();
    }

    private void DisplayMessagesInfo()
    {
        countOfDialogs = serializedObject.FindProperty("_countOfModals");
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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_onStart"));
        EditorGUILayout.Space();
    }
}
