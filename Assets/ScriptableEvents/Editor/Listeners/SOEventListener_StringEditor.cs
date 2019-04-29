﻿using UnityEditor;

[CustomEditor(typeof(SOEventListener_String))]
public class SOEventListener_StringEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SOEventListener_String script = (SOEventListener_String)target;

        this.serializedObject.Update();
        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Event"), true);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.LabelField("Event Description", EditorStyles.centeredGreyMiniLabel);

        if (script.Event != null)
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField(script.Event.DescriptionText);
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Response"), true);

        this.serializedObject.ApplyModifiedProperties();
    }
}


