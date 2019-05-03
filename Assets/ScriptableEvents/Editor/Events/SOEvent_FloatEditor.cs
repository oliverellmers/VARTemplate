using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Float))]
public class SOEvent_FloatEditor : Editor
{
    private float tempValue = 0;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Float script = (SOEvent_Float)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.FloatField("Value to use", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}
