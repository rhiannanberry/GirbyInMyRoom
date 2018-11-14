using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScribbleRotate : MonoBehaviour {
	[Range(0f, 15.0f)]
	public float rotateAmount = 10.0f;
	public float frameLength = 0.8f;

	private float timeSum = 0.0f;
	private RectTransform rt;
	private bool cw = true;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSum+=Time.deltaTime;
		if (timeSum >= frameLength) {
			float rot = Random.Range(0, rotateAmount);
			if (cw) {
				rt.rotation = Quaternion.Euler(0,0,-rot);
			} else {
				rt.rotation = Quaternion.Euler(0,0,rot);
				Debug.Log(timeSum);
			}
			cw = !cw;
			timeSum = 0f;
		}
		
	}
}
