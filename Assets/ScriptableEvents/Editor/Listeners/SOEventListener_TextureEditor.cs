using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEventListener_Texture))]
    public class SOEventListener_TextureEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            SOEventListener_Texture script = (SOEventListener_Texture)target;

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
