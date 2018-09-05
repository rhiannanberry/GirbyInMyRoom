using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform);
		Vector3 rot = transform.rotation.eulerAngles;
		//rot -= (Vector3.up + Vector3.right)*180;
		
		transform.rotation = Quaternion.Euler(new Vector3(-rot.x, rot.y+180, rot.z));
	}
}
