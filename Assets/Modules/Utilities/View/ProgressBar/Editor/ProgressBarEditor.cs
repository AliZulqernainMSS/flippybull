using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProgressBar))]
public class ProgressBarEditor : Editor
{
	SerializedProperty value;
	SerializedProperty minValue;
	SerializedProperty maxValue;

	void OnEnable()
	{
		value = serializedObject.FindProperty ("_value");
		minValue = serializedObject.FindProperty ("_minValue");
		maxValue = serializedObject.FindProperty ("_maxValue");
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		EditorGUILayout.LabelField ("Options", EditorStyles.boldLabel);
		EditorGUILayout.Slider (value, minValue.floatValue, maxValue.floatValue, new GUIContent("Value"));
		EditorGUILayout.PropertyField (minValue, new GUIContent("Min Value"));
		EditorGUILayout.PropertyField (maxValue, new GUIContent("Max Value"));
        serializedObject.ApplyModifiedProperties();
    }
}
