using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOEvent_GameObject))]
public class SOEvent_GameObjectEditor : Editor
{
    private GameObject tempValue = null;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SOEvent_GameObject script = (SOEvent_GameObject)target;

        EditorGUILayout.LabelField("Event Description");
        EditorStyles.textField.wordWrap = true;
        script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

        script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

        if (Application.isPlaying)
        {
            tempValue = (GameObject)EditorGUILayout.ObjectField("Value to use", tempValue, typeof(GameObject), true);

            if (GUILayout.Button("Raise Event"))
            {
                script.Raise(tempValue);
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(script);
    }
}