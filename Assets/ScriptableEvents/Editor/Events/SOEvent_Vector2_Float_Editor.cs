using UnityEngine;
using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEvent_Vector2_Float))]
    public class SOEvent_Vector2_Float_Editor : Editor
    {
        private Vector2 tempValue = Vector3.zero;
        private float floatValue = 0;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SOEvent_Vector2_Float script = (SOEvent_Vector2_Float)target;

            EditorGUILayout.LabelField("Event Description");
            EditorStyles.textField.wordWrap = true;
            script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

            script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

            if (Application.isPlaying)
            {
                tempValue = EditorGUILayout.Vector2Field("Value to use", tempValue);

                floatValue = EditorGUILayout.FloatField("Float Value to use", floatValue);

                if (GUILayout.Button("Raise Event"))
                {
                    script.Raise(tempValue, floatValue);
                }
            }

            serializedObject.ApplyModifiedProperties();

            EditorUtility.SetDirty(script);
        }
    }
}
