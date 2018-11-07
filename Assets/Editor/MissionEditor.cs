using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Mission myTarget = (Mission)target;

        myTarget.missionIcon = (Sprite)EditorGUILayout.ObjectField("Mission Icon", myTarget.missionIcon, typeof(Sprite), true);
        myTarget.bugIcon = (Sprite)EditorGUILayout.ObjectField("Bug Icon", myTarget.bugIcon, typeof(Sprite), true);

        myTarget.summary = EditorGUILayout.TextField("Summary", myTarget.summary);
        myTarget.details = EditorGUILayout.TextField("Details", myTarget.details);
        EditorGUILayout.LabelField("Condition Met: ", myTarget.ConditionMet ? "true" : "false");
        EditorGUILayout.LabelField("Items Count: ", (myTarget.ItemsCount).ToString());
    }
}