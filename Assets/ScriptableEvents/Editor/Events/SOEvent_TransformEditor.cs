using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_Transform))]
public class SOEvent_TransformEditor : Editor
{
    private Transform tempValue = null;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_Transform script = (SOEvent_Transform)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = (Transform)EditorGUILayout.ObjectField("Value to use", tempValue, typeof(Transform), false);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}

