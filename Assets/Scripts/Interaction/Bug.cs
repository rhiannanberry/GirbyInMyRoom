using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Interactable {
	// Use this for initialization
	private bool inDialogue = false;
	private Interactable me;

	new void Start () {
		base.Start();
		base.collisionBasedTrigger = false;
		me = transform.GetComponent<Interactable>();
	}
	
	// Update is called once per frame
	new void Update () {

        if(collisionBasedTrigger || (!collisionBasedTrigger && Inputs.interact) && (inRange == me)) {
            ((Bug)inRange).TriggerInteractable();
        }
		base.Update();
	}

	public new void TriggerInteractable() {
		//idk how this is rly gonna work yet
		base.TriggerInteractable();
		States.interacting = triggered;
		
	}
}
