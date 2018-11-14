using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public followcam cam;

	public static PlayerController playerController;

	BallMove ball;

	void Start () {
		playerController = GetComponent<PlayerController>();
		//cam = GetComponentInChildren<CameraMain>();
		cam = GetComponentInChildren<followcam>();
		ball = GetComponentInChildren<BallMove>();
	}

	void FixedUpdate() {
		Vector3 camForward = Vector3.Scale(cam.transform.forward, new Vector3(1,0,1)).normalized;
        Vector3 move = (Inputs.moveDirection.y*camForward + Inputs.moveDirection.x*cam.transform.right).normalized;
		ball.Move(move, Inputs.jump);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		ball.LockBallPosition(cam.IsLocked());
	}

	public void LockCameraToPoint(Transform point) {
		Debug.Log("Locking Camera onto " + point.name + " at " + point.position);
		ball.LockBallPosition(true);
		cam.LockCameraInFrontOfPoint(point);
	}

	public void UnlockCamera() {
		Debug.Log("Camera unlocked");
		cam.UnlockCamera();
		//ball.LockBallPosition(false);
	}

	public void LockDialogCamera(Transform point) {

	}
}
