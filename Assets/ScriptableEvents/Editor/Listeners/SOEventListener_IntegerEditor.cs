using UnityEditor;

[CustomEditor(typeof(SOEventListener_Integer))]
public class SOEventListener_IntegerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SOEventListener_Integer script = (SOEventListener_Integer)target;

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
