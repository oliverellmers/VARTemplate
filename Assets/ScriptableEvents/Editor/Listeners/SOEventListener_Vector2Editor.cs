using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEventListener_Vector2))]
    public class SOEventListener_Vector2Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            SOEventListener_Vector2 script = (SOEventListener_Vector2)target;

            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Event"), true);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("Event Description", EditorStyles.centeredGreyMiniLabel);

            if (script.Event != null)
            {
                EditorStyles.label.wordWrap = true;
                EditorGUILayout.LabelField(script.Event.DescriptionText);
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Response"), true);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
