using UnityEditor;

namespace ScriptableEvents
{
    [CustomEditor(typeof(SOEventListener_GameObject))]
    public class SOEventListener_GameObjectEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            SOEventListener_GameObject script = (SOEventListener_GameObject)target;

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