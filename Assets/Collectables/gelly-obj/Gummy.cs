using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gummy : MonoBehaviour {

	public Transform destination;
	public float flyTime=1f;
	private bool triggered = false, started = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered && !started && !States.interacting) {
			GetComponent<MeshRenderer>().enabled = true;
			GetComponent<BoxCollider>().enabled = true;
			StartCoroutine(AnimationUtilities.MoveTo3D(transform, destination.position, flyTime));
			triggered = false;
			started = true;
		}
	}

	public void ShowGummy() {
		triggered = true;
	}
}
