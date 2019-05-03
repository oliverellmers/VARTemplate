using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Bool))]
public class SOEvent_BoolEditor : Editor
{
    private bool tempValue = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Bool script = (SOEvent_Bool)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.Toggle("Value to use", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}

