using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager {
	public Queue<InteractableEvent> sequentialEvents;
	public List<InteractableEvent> events;

	public EventManager() {
		sequentialEvents = new Queue<InteractableEvent>();
		events = new List<InteractableEvent>();
	}

	public void AddSequentialEvent(InteractableEvent e) {
		sequentialEvents.Enqueue(e);
	}
	public void AddSequentialEvent(Func<bool> condition, Func<bool> action) {
		AddSequentialEvent(new InteractableEvent(condition, action));
	}

	public void AddEvent(InteractableEvent e) {
		events.Add(e);
	}
	public void AddEvent(Func<bool> condition, Func<bool> action) {
		AddEvent(new InteractableEvent(condition, action));
	}

	public void CheckSequential() {
		if(sequentialEvents.Count > 0 && sequentialEvents.Peek().CheckEvent()) {
			sequentialEvents.Dequeue();
		}
	}

	public void CheckNonsequential() {
		if (events.Count > 0) {
			List<InteractableEvent> notTriggered = new List<InteractableEvent>();
			for (int i = 0; i < events.Count; i++) {
				if (!events[i].CheckEvent()) {
					notTriggered.Add(events[i]);
				}
			}
			events = notTriggered; 
		}
	}

	public void CheckEvents() {
		CheckSequential();
		CheckNonsequential();
	}
}

public class InteractableEvent{
	Func<bool> condition, action;
	public InteractableEvent(Func<bool> condition, Func<bool> action) {
		this.condition = condition;
		this.action = action;
	}
	public bool CheckEvent() {
		return (condition.Invoke()) ? action.Invoke() : false;
	}
}