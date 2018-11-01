using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : Bug {

    new void Start () {
		base.Start();
		base.collisionBasedTrigger = false;
	}
	
	// Update is called once per frame
	new void Update () {
        bool interact = Input.GetKeyDown(KeyCode.E);

        if(collisionBasedTrigger || (!collisionBasedTrigger && interact) && (inRange != null) && (inRange is Roomba)) {
            Debug.Log("Triggering a Roomba?");
            ((Roomba)inRange).TriggerInteractable();
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
}
