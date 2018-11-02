using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Inputs.ResetAll();
	}
	
	// Update is called once per frame
	void Update () {
		Inputs.togglePause = Input.GetKeyUp(KeyCode.Escape);
		Inputs.interact = Input.GetKeyUp(KeyCode.E);
		Inputs.leftClick = Input.GetMouseButtonUp(0);
		Inputs.jump = Input.GetKeyUp(KeyCode.Space);
		Inputs.moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Inputs.fwd = Inputs.moveDirection.y > 0;
		Inputs.bwd = Inputs.moveDirection.y < 0;
		Inputs.left = Inputs.moveDirection.x < 0;
		Inputs.right = Inputs.moveDirection.x > 0;
		Inputs.mouseVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		Debug.Log(States.paused);
	}
}
