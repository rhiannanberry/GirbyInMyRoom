using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour {
	public Transform toTrack;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		if (toTrack == null) {
			Debug.LogError("Camera not tracking anything!");
		} else {
			offset = transform.position - toTrack.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = offset + toTrack.position;
	}
}
