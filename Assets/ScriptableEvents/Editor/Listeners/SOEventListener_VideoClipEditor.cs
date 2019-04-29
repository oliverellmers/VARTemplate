using UnityEngine;
using UnityEditor;
using UnityEngine.Video;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEvent_VideoClip))]
    public class SOEventListener_VideoClipEditor : Editor
    {
        private VideoClip tempValue = null;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SOEvent_VideoClip script = (SOEvent_VideoClip)target;

            EditorGUILayout.LabelField("Event Description");
            EditorStyles.textField.wordWrap = true;
            script.DescriptionText = EditorGUILayout.TextField(script.DescriptionText, GUILayout.MinHeight(100));

            script.ShowDebugMessages = EditorGUILayout.Toggle("Send Debug Messages", script.ShowDebugMessages);

            if (Application.isPlaying)
            {
                tempValue = (VideoClip)EditorGUILayout.ObjectField("Value to use", tempValue, typeof(VideoClip), false);

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
