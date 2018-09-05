using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GirbyShader : MonoBehaviour {

	private Transform ground;
	// Use this for initialization
	void Start () {
	}
	
	
	void Update () {
		Material m = GetComponent<Renderer>().sharedMaterial;
		m.SetFloat("_SquishY", transform.position.y);
		if (ground != null) {
			float newDist = transform.position.y - ground.position.y;
			m.SetFloat("_SquishDistance", newDist);
		}
	}

	void OnTriggerEnter(Collider c) {
		if (ground == null) {
			ground = c.transform;
		}
		Debug.Log("hhhhh");
	}
	void OnTriggerExit(Collider c) {
		if (ground == c.transform) {
			ground = null;
		}
		Debug.Log("hhhhh");
	}
}
