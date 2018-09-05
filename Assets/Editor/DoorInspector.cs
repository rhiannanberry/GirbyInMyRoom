using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorInspector : Editor {
	private void OnSceneGUI() {
		Door door = target as Door;
		Handles.color = Color.white;
		Transform handleTransform = door.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

		Vector3 widthHeight;

		Vector3 pos;

		//these might need to be added to the door object as private parameters
		Vector3 bLeft = handleTransform.TransformPoint(Vector3.zero);
		Vector3 tLeft =  handleTransform.TransformPoint(door.height*Vector3.up);
		Vector3 bRight = handleTransform.TransformPoint(door.width*Vector3.right);
		Vector3 tRight = handleTransform.TransformPoint(door.width*Vector3.right+door.height*Vector3.up);

		//basically highlighting the outline of the door

		Handles.DrawLine(bLeft, bRight);
		Handles.DrawLine(tLeft, tRight);
		Handles.DrawLine(bLeft, tLeft);
		Handles.DrawLine(bRight, tRight);

		EditorGUI.BeginChangeCheck();
		//pos = Handles.DoPositionHandle(bLeft, handleRotation);
		widthHeight = Handles.DoPositionHandle(tRight, handleRotation);
		Vector3 diag = handleTransform.InverseTransformPoint(widthHeight);
		if (EditorGUI.EndChangeCheck() || handleTransform.hasChanged || diag.x != door.width || diag.y != door.height) {
			if (handleTransform.hasChanged) {
				handleTransform.hasChanged = false;
			}
			Undo.RecordObject(door, "Scale door");
			EditorUtility.SetDirty(door);
			
			//door.transform.position = pos;
			door.ClampedManualUpdate(diag.x, diag.y);
			//door.width = diag.x;
			//door.height = diag.y;
		}
	}
}
