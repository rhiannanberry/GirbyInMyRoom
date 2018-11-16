using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionsUISetup : MonoBehaviour {
	public Transform missions;
	private int missionChildCount=0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int numChildren = transform.childCount;
		for (int i=numChildren; i > 0; i--) {
			transform.GetChild(numChildren-i).GetComponent<Canvas>().sortingOrder = i+5;
			if (transform.GetChild(numChildren-i).GetComponent<MissionButton>().hovering) {
				transform.GetChild(numChildren-i).GetComponent<Canvas>().sortingOrder = numChildren+6;
			}
		}
	}
}
