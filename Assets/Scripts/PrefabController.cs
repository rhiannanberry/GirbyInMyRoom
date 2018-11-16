using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour {
	//UI
	public Transform missionDetails;
	public Transform missionDetailsRight;
	public Transform missionDetailsBottom;
	public Transform missionDetailsBottomRight;
	// Use this for initialization
	void Start () {
		Prefabs.missionDetailsPrefab = missionDetails;
		Prefabs.missionDetailsRightPrefab = missionDetailsRight;
		Prefabs.missionDetailsBottomPrefab = missionDetailsBottom;
		Prefabs.missionDetailsBottomRightPrefab = missionDetailsBottomRight;
	}
}

public static class Prefabs {
	public static Transform missionDetailsPrefab;
	public static Transform missionDetailsRightPrefab;
	public static Transform missionDetailsBottomPrefab;
	public static Transform missionDetailsBottomRightPrefab;
}
