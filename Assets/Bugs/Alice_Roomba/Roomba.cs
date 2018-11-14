using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : Bug {

	public Transform hoverEndLocation;
	public float speed = 5.0f;
	private Vector3 startLocation;
	private bool hoverActivated = false, up = true;

    new void Start () {
		base.Start();
		base.collisionBasedTrigger = false;
		startLocation = transform.position;
	}
	
	// Update is called once per frame
	new void Update () {
        if(collisionBasedTrigger || (!collisionBasedTrigger && Inputs.interact) && (inRange != null) && (inRange is Roomba)) {
            Debug.Log("Triggering a Roomba?");
            ((Roomba)inRange).TriggerInteractable();
        }
		if(hoverActivated && !States.interacting) {
			if(up) {
				if (transform.position == hoverEndLocation.position) {
					up = false;
				} else {
					GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, hoverEndLocation.position, speed*Time.deltaTime));
				}
			} else {
				if (transform.position == startLocation) {
					up = true;
				} else {
					GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(transform.position, startLocation, speed*Time.deltaTime));
				}
			}
		}
		base.Update();
	}

    public new void TriggerInteractable() {
        Debug.Log("ROOMBA TRIGGERED");
        //idk how this is rly gonna work yet
        base.TriggerInteractable();
        
        
    }

	new void OnTriggerEnter(Collider other)
	{
        Debug.Log("ROOMBA ENTERED!");
        if (other.CompareTag("Player")) {
			Debug.Log("In range!!");
			inRange = transform.GetComponent<Roomba>();
			if (ui != null)
				rightPanel.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}

	public void ActivateRoombaHover() {
		hoverActivated = true;
	}
}
