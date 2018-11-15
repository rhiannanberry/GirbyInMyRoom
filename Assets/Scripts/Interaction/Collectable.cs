using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : Interactable {

	public float rotateSpeed = 10.0f;
	public float periodLength = 1.0f;
	public Vector3 hoverDelta = new Vector3(0, 0.05f, 0);
	protected Mission mission;
	private float timeDelta, randStart;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		base.Start();
		base.collisionBasedTrigger = true;
		mission = transform.parent.GetComponent<Mission>();

		UnityEvent killSelf = new UnityEvent();
		UnityEvent updateUI = new UnityEvent();
		killSelf.AddListener(SetInactive);
		updateUI.AddListener(UpdateUI);
		base.interactionManager.AddEvent(null, updateUI);
		base.interactionManager.AddEvent(null,killSelf);
		//x/2pi = currtime/periodlength
		//x = currtime*2pi/periodlength
		startPosition = transform.position;
		randStart = Random.Range(0, 1.0f);
		timeDelta = 2*Mathf.PI/periodLength;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
		Hover();
		Spin();
		
	}

	private void SetInactive() {
		Interactable.inRange = null;
		gameObject.SetActive(false);
	}

	private void UpdateUI() {
		HUDActions.UpdateMission(mission);
	}

	private void Hover() {
		float delta = Mathf.Sin(((Time.time+randStart)%periodLength)*timeDelta);
		transform.position = startPosition + delta*hoverDelta;
	}

	private void Spin() {
		transform.Rotate ( Vector3.up * ( rotateSpeed * Time.deltaTime ), UnityEngine.Space.World );
	}
}
