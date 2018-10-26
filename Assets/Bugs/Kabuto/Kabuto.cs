using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kabuto : Bug {

    private Animator anim;
    // Use this for initialization


    new void Start () {
		base.Start();
		anim = GetComponent<Animator>();
		base.collisionBasedTrigger = false;
	}
	
	// Update is called once per frame
	new void Update () {
        bool interact = Input.GetKeyDown(KeyCode.E);

        if(collisionBasedTrigger || (!collisionBasedTrigger && interact) && (inRange != null) && (inRange is Kabuto)) {
            Debug.Log("Triggering a kabuto?");
            ((Kabuto)inRange).TriggerInteractable();
        }
		base.Update();
	}

    public new void TriggerInteractable() {
        anim.SetTrigger("trigger");
        Debug.Log("KABUTO TRIGGERED");
        //idk how this is rly gonna work yet
        base.TriggerInteractable();
        
        
    }

	new void OnTriggerEnter(Collider other)
	{
        Debug.Log("KABUTO ENTERED!");
        if (other.CompareTag("Player")) {
			Debug.Log("In range!!");
			inRange = transform.GetComponent<Kabuto>();
			if (ui != null)
				rightPanel.SetActive(true);
		}
	}

	void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
	}
}
