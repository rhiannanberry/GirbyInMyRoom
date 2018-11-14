using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

	
	Rigidbody rb;

	public bool useTorque = true;
	public float movePower = 5;
	public float jumpPower = 2;
	public float maxAngularVelocity = 25;

	private Vector3 gravityDirection = Vector3.down;
	private const float groundRayLength = 1f;
	private bool falling = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.maxAngularVelocity = maxAngularVelocity;
	}

	public void Move(Vector3 moveDirection, bool jump) {
		Vector3 force = Vector3.zero;
		if (useTorque) { // If using torque to rotate the ball...
			rb.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*movePower); //add torque around the axis defined by the move direction.
		} else {
			force = moveDirection*movePower;
			//rb.AddForce(moveDirection*movePower); //Otherwise add force in the move direction.
		}

		if (Physics.Raycast(transform.position, -Vector3.up, groundRayLength) && jump)
		{
			// ... add force in upwards.
			force = Vector3.zero;
			rb.MovePosition(transform.position + moveDirection * Time.deltaTime);
			rb.AddForce(Vector3.up*jumpPower, ForceMode.Impulse);
		}
		rb.AddForce(force); 
	}

	public void LockBallPosition(bool toLock) {
		if(toLock) {
			rb.constraints = RigidbodyConstraints.FreezeAll;
		} else {
			rb.constraints = RigidbodyConstraints.None;

		}
	}

	public void OnCollisionEnter(Collision col) {
		falling = false;
		if (col.gameObject.layer == 9) {
			gravityDirection = Vector3.down;//-col.contacts[0].normal;
			Physics.gravity = 9.81f* gravityDirection;
			
			
		}
		
	}

	
}
