using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_String))]
public class SOEvent_StringEditor : Editor
{
    private string tempValue = "Enter string here";

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_String script = (SOEvent_String)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.TextField("String to pass", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}
