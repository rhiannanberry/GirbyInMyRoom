using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractionManager {
	public Queue<InteractableEvent> sequentialEvents;
	public List<Interaction> events;

	public InteractionManager() {
		sequentialEvents = new Queue<InteractableEvent>();
		events = new List<Interaction>();
	}

	public void AddSequentialEvent(params InteractableEvent[] es) {
		foreach(InteractableEvent e in es) {
			sequentialEvents.Enqueue(e);
		}
	}
	public void AddSequentialEvent(Func<bool> condition, Func<bool> action) {
		//AddSequentialEvent(new InteractableEvent(condition, action));
	}

	public void AddEvent(params Interaction[] es) {
		foreach(Interaction e in es) {
			events.Add(e);
		}
	}
	public void AddEvent(Func<bool> condition, Func<bool> action) {
		//AddEvent(new InteractableEvent(condition, action));
	}

	public void CheckSequential() {
		if(sequentialEvents.Count > 0 && sequentialEvents.Peek().CheckEvent()) {
			sequentialEvents.Dequeue();
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


[System.Serializable]
public class InteractableEvent {
	public ScriptableObject condition;

	public UnityEvent action;
	public InteractableEvent(ScriptableObject condition, UnityEvent action) {
		this.condition = condition;
		this.action = action;
	}
	public bool CheckEvent() {
		return true;//(condition.Invoke()) ? action.Invoke() : false;
	}
}

