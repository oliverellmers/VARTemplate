using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Vector2))]
public class SOEvent_Vector2Editor : Editor
{
    private Vector2 tempValue = Vector3.zero;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Vector2 script = (SOEvent_Vector2)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = EditorGUILayout.Vector2Field("Value to use", tempValue);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}
