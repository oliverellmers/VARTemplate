﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Integer))]
public class SOEvent_IntegerEditor : Editor
{
    private int tempValue = -1;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Integer script = (SOEvent_Integer)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.IntField("Value to use", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}
