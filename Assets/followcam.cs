using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcam : MonoBehaviour {
	public Transform target;
	public float rotationSpeed = 5.0f;
	[Range(0.01f, 1.0f)]
	public float followSmoothFactor = 0.5f;

	[Range(0.01f, 1.0f)]
	public float lockSmoothFactor = 0.5f;

	private Transform _target;
	private Vector3 _offset, offset;
	private Quaternion camTurnAngle, camPitchAngle;
	private bool locked = false, resetting = false;

	// Use this for initialization
	void Start () {
		_offset = offset = transform.position - target.position;
		_target = target;
	}

	private void LateUpdate()
	{
		if (resetting) {
			if (OffsetReset(target.position, _offset, .01f)) {
				transform.position = target.position+offset;
				resetting = false;
				locked = false;
			}
		}
		
		if(!locked) {//TODO: change this to use Inputs later
			camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X")*rotationSpeed, Vector3.up);
			camPitchAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse Y")*rotationSpeed,transform.right);
			offset = camPitchAngle * camTurnAngle * offset ;
			Follow(offset,_target,followSmoothFactor);
		} else {
			Follow(offset,target,lockSmoothFactor);
		}
	}

	private void Follow(Vector3 offset, Transform target, float smoothing) {
		Vector3 desiredPosition = target.position+offset;
		Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothing);

		transform.position = smoothedPosition;
		transform.LookAt(target);
	}

	private bool OffsetReset(Vector3 target, Vector3 originalOffset, float distanceFuzz) {
		return Mathf.Abs(Vector3.Distance(transform.position - target, originalOffset)) < distanceFuzz;
	}

	public void LockCameraInFrontOfPoint(Transform point) {
		locked = true;
		_offset = offset;

		Vector3 newCameraPosition = point.forward*1.5f + point.position;
		if (Vector3.Distance(newCameraPosition, transform.position) > Vector3.Distance(point.position - point.forward*1.5f, transform.position)) {
			newCameraPosition = point.position - point.forward*1.5f;
		}

		offset = newCameraPosition - point.position;
		target = point;
	}
	public void UnlockCamera() {
		resetting = true;
		target = _target;
		offset = _offset;
	}

	public bool IsLocked() {
		return locked;
	}
}
