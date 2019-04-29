using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Vector3))]
public class SOEvent_Vector3Editor : Editor
{
    private Vector3 tempValue = Vector3.zero;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Vector3 script = (SOEvent_Vector3)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.Vector3Field("Value to use", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}
