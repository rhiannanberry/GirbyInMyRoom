using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionManager {

	[SerializeField]
	private int sequenceLocation = 0;
	public List<Interaction> sequentialEvents;
	public List<InteractionNonSequenced> events;

	public InteractionManager() {
		sequentialEvents = new List<Interaction>();
		events = new List<InteractionNonSequenced>();
	}

	public void AddSequentialEvent(params Interaction[] es) {
		foreach(Interaction e in es) {
			sequentialEvents.Add(e);
		}
	}
	public void AddSequentialEvent(Func<bool> condition, Func<bool> action) {
		//AddSequentialEvent(new InteractableEvent(condition, action));
	}

	public void AddEvent(params Interaction[] es) {
		foreach(InteractionNonSequenced e in es) {
			events.Add(e);
		}
	}
	public void AddEvent(Func<bool> condition, Func<bool> action) {
		//AddEvent(new InteractableEvent(condition, action));
	}

	public void CheckSequential() {
		if(sequentialEvents.Count > 0 && sequenceLocation < sequentialEvents.Count) {
			if (sequentialEvents[sequenceLocation].CheckAndReact()) {
				sequenceLocation++;
			}
		}
	}

	public void CheckNonsequential() {
		if (events.Count > 0) {
			//List<InteractableEvent> notTriggered = new List<InteractableEvent>();
			for (int i = 0; i < events.Count; i++) {
				events[i].CheckAndReact();
				//if (!events[i].CheckEvent()) {
				//	notTriggered.Add(events[i]);
				//}
			}
			//events = notTriggered; 
		}
	}

	public void CheckEvents() {
		CheckSequential();
		CheckNonsequential();
	}
}