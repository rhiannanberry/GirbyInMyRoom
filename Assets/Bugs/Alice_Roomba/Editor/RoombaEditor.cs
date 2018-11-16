using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Roomba))]
public class RoombaEditor : BugEditor {
	private SerializedProperty hoverLoc, spd;
	public void OnEnable() {
        base.OnEnable();
		hoverLoc = serializedObject.FindProperty("hoverEndLocation");
		spd = serializedObject.FindProperty("speed");
	}

	public override void OnInspectorGUI() {
        //DrawDefaultInspector();
		serializedObject.Update();
		UpdateProperties();
		base.UpdateProperties();
		serializedObject.ApplyModifiedProperties();
	}

	public new void UpdateProperties() {
		EditorGUILayout.PropertyField(hoverLoc); 
		EditorGUILayout.PropertyField(spd);
	}
}
