using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[CustomEditor(typeof(Interactable))]
public class InteractionEditor : Editor {
	private ReorderableList seqList;
    private ReorderableList evList;
	
	public void OnEnable() {
        DrawSeqEv();
        DrawUnseqEv();
	}

    public void DrawSeqEv() {
        SerializedProperty seqEv = serializedObject.FindProperty("interactionManager.sequentialEvents");
		seqList = new ReorderableList(serializedObject, 
        		seqEv, 
        		true, true, true, true);

        seqList.drawHeaderCallback = rect => {
                EditorGUI.LabelField (rect, "Sequential Events");
        };

        List<float> heights = new List<float> (seqEv.arraySize);

         seqList.drawElementCallback = 
            (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = seqList.serializedProperty.GetArrayElementAtIndex(index);
                    SerializedProperty action = element.FindPropertyRelative("action");
                    float actionHeight = EditorGUI.GetPropertyHeight(action);
                        bool foldout = isActive;
                        float height = (4*EditorGUIUtility.singleLineHeight + actionHeight) ;
 
                        try {
                                heights [index] = height;
                        } catch (ArgumentOutOfRangeException e) {
                                Debug.LogWarning (e.Message);
                        } finally {
                                float[] floats = heights.ToArray ();
                                Array.Resize (ref floats, seqEv.arraySize);
                                heights = floats.ToList ();
                        }
 
                        float margin = EditorGUIUtility.singleLineHeight*1.5f;
                        rect.width -= margin;
                        
                        rect.y += margin;
                        rect.height = EditorGUIUtility.singleLineHeight;
                        
                        EditorGUI.PropertyField(rect,element.FindPropertyRelative("conditionObject"), GUIContent.none);
                        rect.y+=margin;
                        rect.height = actionHeight;
                        
                        
                        EditorGUI.PropertyField(rect,action, GUIContent.none);
                        //EditorGUI.ObjectField (rect, element, GUIContent.none);
            
        };

        seqList.elementHeightCallback = (index) => {
                        Repaint ();
                        float height = 0;
 
                        try {
                                height = heights [index];
                        } catch (ArgumentOutOfRangeException e) {
                                Debug.LogWarning (e.Message);
                        } finally {
                                float[] floats = heights.ToArray ();
                                Array.Resize (ref floats, seqEv.arraySize);
                                heights = floats.ToList ();
                        }
 
                        return height;
                };

    }

    public void DrawUnseqEv() {
        SerializedProperty ev = serializedObject.FindProperty("interactionManager.events");
		evList = new ReorderableList(serializedObject, 
        		ev, 
        		true, true, true, true);

        evList.drawHeaderCallback = rect => {
                EditorGUI.LabelField (rect, "Events");
        };

        List<float> heights = new List<float> (ev.arraySize);

         evList.drawElementCallback = 
            (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = evList.serializedProperty.GetArrayElementAtIndex(index);
                    SerializedProperty action = element.FindPropertyRelative("action");
                    SerializedProperty repeat = element.FindPropertyRelative("repeat");
                    float actionHeight = EditorGUI.GetPropertyHeight(action);
                        bool foldout = isActive;
                        float height = (6*EditorGUIUtility.singleLineHeight + actionHeight) ;
 
                        try {
                                heights [index] = height;
                        } catch (ArgumentOutOfRangeException e) {
                                Debug.LogWarning (e.Message);
                        } finally {
                                float[] floats = heights.ToArray ();
                                Array.Resize (ref floats, ev.arraySize);
                                heights = floats.ToList ();
                        }
 
                        float margin = EditorGUIUtility.singleLineHeight*1.5f;
                        
                        float startx = rect.x;
                        float fullWidth = rect.width;

                        rect.y += margin;
                        rect.height = EditorGUIUtility.singleLineHeight;
                        rect.width = 3*fullWidth/4 - 2*margin;
                        
                        EditorGUI.PropertyField(rect,element.FindPropertyRelative("conditionObject"), GUIContent.none);
                        
                        rect.x += margin + rect.width;
                        rect.width = rect.height;
                        EditorGUI.PropertyField(rect,element.FindPropertyRelative("repeat"), GUIContent.none);

                        rect.x += margin;
                        rect.y += 2;
                        rect.width = fullWidth/4 - 2*margin;
                        EditorGUI.LabelField(rect,"Repeat");

                        rect.x = startx;
                        rect.y+=margin-2;
                        rect.width = fullWidth- margin;
                        rect.height = actionHeight;
                        
                        
                        EditorGUI.PropertyField(
                            rect,
                            action, GUIContent.none);
                        //EditorGUI.ObjectField (rect, element, GUIContent.none);
            
        };

        evList.elementHeightCallback = (index) => {
                        Repaint ();
                        float height = 0;
 
                        try {
                                height = heights [index];
                        } catch (ArgumentOutOfRangeException e) {
                                Debug.LogWarning (e.Message);
                        } finally {
                                float[] floats = heights.ToArray ();
                                Array.Resize (ref floats, ev.arraySize);
                                heights = floats.ToList ();
                        }
 
                        return height;
                };
    }
	
	public override void OnInspectorGUI() {
        //DrawDefaultInspector();
		serializedObject.Update();
		UpdateProperties();
		serializedObject.ApplyModifiedProperties();
	}

    public void UpdateProperties() {
        seqList.DoLayoutList();
        evList.DoLayoutList();
    }
}