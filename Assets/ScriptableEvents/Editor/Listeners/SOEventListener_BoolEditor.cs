using UnityEditor;

[CustomEditor(typeof(SOEventListener_Bool))]
public class SOEventListener_BoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SOEventListener_Bool script = (SOEventListener_Bool)target;

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

