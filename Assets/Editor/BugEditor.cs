using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[CustomEditor(typeof(Bug))]
public class BugEditor : InteractionEditor {
    public void OnEnable() {
        base.OnEnable();
	}

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
	}

}