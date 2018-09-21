using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : Interactable {

	// Use this for initialization
	void Start () {
		base.Start();
		base.collisionBasedTrigger = true;
		UnityEvent killSelf = new UnityEvent();
		killSelf.AddListener(SetInactive);
		base.interactionManager.AddEvent(null,killSelf);
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		
	}

	private void SetInactive() {
		gameObject.SetActive(false);
	}
}
