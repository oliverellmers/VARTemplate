using UnityEngine;
using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEvent_Texture))]
    public class SOEvent_TextureEditor : Editor
    {
        private Texture tempValue = null;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SOEvent_Texture script = (SOEvent_Texture)target;

            EditorGUILayout.LabelField("Event Description");
            EditorStyles.textField.wordWrap = true;
            script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

            script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

            if (Application.isPlaying)
            {
                tempValue = (Texture)EditorGUILayout.ObjectField("Value to use", tempValue, typeof(Texture), false);

                if (GUILayout.Button("Raise Event"))
                {
                    script.Raise(tempValue);
                }
            }

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(script);
        }
    }
}
