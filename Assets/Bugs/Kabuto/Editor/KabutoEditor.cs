using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Kabuto))]
public class KabutoEditor : BugEditor {
	public void OnEnable() {
        base.OnEnable();
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();
		base.UpdateProperties();
		serializedObject.ApplyModifiedProperties();
	}

}
