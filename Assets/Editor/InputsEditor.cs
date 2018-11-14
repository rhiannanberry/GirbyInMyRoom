using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputController))]
public class InputsEditor : Editor {

	 public override void OnInspectorGUI()
    {
		EditorGUILayout.LabelField("INPUTS");
		EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("Interact", Inputs.interact ? "true" : "false");
		EditorGUILayout.LabelField("Toggle Pause", Inputs.togglePause ? "true" : "false");
		EditorGUILayout.LabelField("Left Click", Inputs.leftClick? "true" : "false");
		EditorGUILayout.LabelField("Forward", Inputs.fwd ? "true" : "false");
		EditorGUILayout.LabelField("Backward", Inputs.bwd ? "true" : "false");
		EditorGUILayout.LabelField("Left", Inputs.left ? "true" : "false");
		EditorGUILayout.LabelField("Right", Inputs.right ? "true" : "false");
		EditorGUILayout.LabelField("Jump", Inputs.jump ? "true" : "false");
		EditorGUILayout.LabelField("Move Direction", Inputs.moveDirection.ToString());
		EditorGUILayout.LabelField("Mouse Velocity", Inputs.mouseVelocity.ToString());
		EditorGUILayout.LabelField("Camera Velocity", Inputs.cameraVelocity.ToString());

		EditorGUI.indentLevel--;
		EditorGUILayout.LabelField("");
		EditorGUILayout.LabelField("STATES");
		EditorGUI.indentLevel++;
		EditorGUILayout.LabelField("Paused", States.paused ? "true" : "false");
        EditorGUILayout.LabelField("Interacting", States.interacting ? "true" : "false");
		EditorGUILayout.LabelField("Menu Open", States.missionOpen ? "true" : "false");
		//EditorGUILayout.LabelField("Items Count: ", (myTarget.ItemsCount).ToString());

		EditorGUI.indentLevel--;
		if (EditorApplication.isPlaying)
        {
			this.Repaint();
		}
    }
}
