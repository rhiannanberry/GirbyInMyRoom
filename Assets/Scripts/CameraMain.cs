using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour {

	public Transform player;
	public Transform room;

	public Transform girbyModel;

	private Vector3 offset, newCamPos, origCamPos;
	private float distance;

	private bool locked = false, resetting = false;
	private Transform point;
	private Quaternion oldXRot, oldYRot, origCamRot;
	void Start () {
		offset = transform.position - player.position;
		distance = offset.magnitude;
		//transform.LookAt(player, Vector3.up);
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (Input.GetKeyDown(KeyCode.E) && Interactable.inRange != null) {
			if (Interactable.inRange is Bug) {
				((Bug)Interactable.inRange).TriggerInteractable();
			} else {
				Interactable.inRange.TriggerInteractable();

			}
		}

		Transform xRot = transform.parent;
		
		if (resetting) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, origCamRot, 20f*Time.deltaTime);
			transform.position = Vector3.MoveTowards(transform.position, origCamPos, Time.deltaTime*10);
			//player.rotation = Quaternion.RotateTowards(player.rotation, oldYRot, 10f*Time.deltaTime);
			if (origCamRot == transform.rotation && origCamPos == transform.position) {
				resetting = false;
			}
		} else if (!locked) {
			player.Rotate(Vector3.up*1.5f*Input.GetAxis("Mouse X"));
			xRot.Rotate(Vector3.right*1.5f*Input.GetAxis("Mouse Y"));
		} else { //locked
			//Vector3 dir = (point.position - transform.position).normalized;
			
			
			transform.position = Vector3.MoveTowards(transform.position, newCamPos, Time.deltaTime*3);
			Quaternion lookRot = Quaternion.LookRotation(point.position-transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 3*Time.deltaTime);
			Quaternion lookRotGirb = Quaternion.LookRotation(point.position-girbyModel.position);
			girbyModel.rotation = Quaternion.Slerp(girbyModel.rotation, lookRotGirb, 10*Time.deltaTime);
		}
		
	}

	public void LockCameraOnPoint(Transform point) {
		locked = true;
		this.point = point;

		origCamPos = transform.position;
		origCamRot = transform.rotation;

		oldXRot = transform.parent.rotation;
		oldYRot = player.rotation;
		float playerYdist = (point.position.y-player.position.y)/2;
		Vector3 playerPos = Vector3.Scale(player.position, new Vector3(1, 0, 1));
		Vector3 pointPos = Vector3.Scale(point.position, new Vector3(1, 0, 1));
		Vector3 camTranslation = playerPos-pointPos;
		
		float playerXZdist = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), new Vector3(point.position.x, 0, point.position.z));
		//xz
		newCamPos = new Vector3(transform.position.x, point.position.y+playerYdist, transform.position.z);// + camTranslation*.5f;
	}

	public void UnlockCamera() {
		locked = false;
		resetting = true;
	}
}
