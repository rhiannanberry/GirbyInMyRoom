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
		if (Application.isEditor) {
			Inputs.togglePause = Input.GetKeyDown(KeyCode.Q);
		} else {
			Inputs.togglePause = Input.GetKeyDown(KeyCode.Escape);
		}
		
		Inputs.interact = Input.GetKeyDown(KeyCode.E);
		Inputs.leftClick = Input.GetMouseButton(0);
		Inputs.jump = Input.GetKeyDown(KeyCode.Space);
		Inputs.moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		Inputs.fwd = Inputs.moveDirection.y > 0;
		Inputs.bwd = Inputs.moveDirection.y < 0;
		Inputs.left = Inputs.moveDirection.x < 0;
		Inputs.right = Inputs.moveDirection.x > 0;
		Inputs.mouseVelocity = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
	
		if (States.paused) {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}
