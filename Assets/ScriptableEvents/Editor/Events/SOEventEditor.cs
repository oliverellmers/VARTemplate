using UnityEngine;
using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEvent))]
    public class SOEventEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SOEvent script = (SOEvent)target;

            EditorGUILayout.LabelField("Event Description");
            EditorStyles.textField.wordWrap = true;
            script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

            script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

            if (Application.isPlaying)
            {
                if (GUILayout.Button("Raise Event"))
                {
                    script.Raise();
                }
            }

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(script);
        }
    }
}

