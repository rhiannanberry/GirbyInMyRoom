using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour {

	public Vector3 hidePath;
	public float hideTime=4f;
	private bool triggered = false, started = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered && !started && !States.interacting) {
			StartCoroutine(AnimationUtilities.MoveTo3D(transform, transform.position+hidePath, hideTime));
			triggered = false;
			started = true;
		}
	}

	public void Disappear() {
		triggered = true;
	}
}
