using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ConditionEvent))]
public class ConditionEventEditor:Editor {
    public override void OnInspectorGUI()
    {
        ConditionEvent myTarget = (ConditionEvent)target;

        myTarget.description = EditorGUILayout.TextField("Details", myTarget.description);
        EditorGUILayout.LabelField("Condition Met: ", myTarget.ConditionMet ? "true" : "false");
    }
}