using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour {
	//UI
	public Transform missionDetails;
	// Use this for initialization
	void Start () {
		Prefabs.missionDetailsPrefab = missionDetails;
	}
}

public static class Prefabs {
	public static Transform missionDetailsPrefab;
}
