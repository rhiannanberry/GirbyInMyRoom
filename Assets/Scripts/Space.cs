using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour {

	List<Quaternion> ql;
	public Stack<Transform> qq;

	// Use this for initialization
	void Start () {
		ql = new List<Quaternion>();
		qq = new Stack<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 torqueAdd = Vector3.zero;
		Vector3 torqueDir = Vector3.zero;
		float horiz = Input.GetAxis("Horizontal");
		float verti = Input.GetAxis("Vertical");
		float highest = 0;
		Transform highestCube = null;
		foreach (Transform child in transform.GetComponentsInChildren<Transform>()) {
			if (highestCube == null || child.position.y > highest)  {
				highestCube = child;
				highest = child.localPosition.y;
			}
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			transform.Rotate(Vector3.down*90, UnityEngine.Space.World);
			qq.Push(highestCube);
			
		} else if (Input.GetKeyDown(KeyCode.D)) {
			transform.Rotate(Vector3.up*90, UnityEngine.Space.World);
			qq.Push(highestCube);
			
		} else if (Input.GetKeyDown(KeyCode.W)) {
			transform.Rotate(Vector3.left*90, UnityEngine.Space.World);
			qq.Push(highestCube);
			
		} else if (Input.GetKeyDown(KeyCode.S)) {
			transform.Rotate(Vector3.right*90, UnityEngine.Space.World);
			qq.Push(highestCube);
			
		}
	}
}
