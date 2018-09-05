using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitch : MonoBehaviour {

	public enum Menu{Main, Settings, Credits};

	public GameObject main, settings, credits;
	public float duration = 0.5f;

	GameObject destination;
	private Vector2 containerPathVector, containerSourcePosition;
	private RectTransform rt;
	private RectTransform currentMenuTransform;
	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		currentMenuTransform = main.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Switch(int destination) {

		switch(destination) {
			case 0:
				this.destination = main;
				break;
			case 1:
				this.destination = settings;
				break;
			case 2:
				this.destination = credits;
				break;
		}

		//current pos - destination pos bc these positions are inside the rect transform
		//that we actually intend on moving, so they must go the opposite direction
		containerPathVector = currentMenuTransform.anchoredPosition - this.destination.GetComponent<RectTransform>().anchoredPosition;
		containerSourcePosition = rt.anchoredPosition;
		StartCoroutine(MenuTransition());
	}

	IEnumerator MenuTransition() {
		float dist = containerPathVector.magnitude;
		Vector2 dir = containerPathVector.normalized;
		float currentTime = 0.0f;
		float currentDist = 0.0f;
		while( currentTime <= duration) {
			/* CORRECT LINEAR:
			currentTime += Time.deltaTime;
			float lerpTime = (currentTime/duration);
			Vector3 currentPos = Vector2.Lerp(sourcePosition, destinationPosition, lerpTime);
			currentTransform.anchoredPosition = currentPos;
			*/
			currentTime += Time.deltaTime;
			currentDist = EaseInOut(currentTime, 0, dist, duration);
			if (currentDist >= dist) {
				rt.anchoredPosition = containerSourcePosition + containerPathVector;
			} else {
				rt.anchoredPosition = containerSourcePosition + currentDist*dir;
			}
			yield return new WaitForFixedUpdate();
		}
		
		currentMenuTransform = destination.GetComponent<RectTransform>();
	}
	float EaseIn(float time, float startValue, float totalDistance, float duration) {
		time /= duration;
		return totalDistance*time*time + startValue;
	}

	float EaseOut(float time, float startValue, float totalDistance, float duration) {
		time /= duration;
		return (-totalDistance)*time*(time-2) + startValue;
	}

	float EaseInOut(float time, float startValue, float totalDistance, float duration) {
		time /= (duration/2);
		if (time < 1) {
			return (totalDistance/2)*time*time + startValue;
		}

		time--;
		return (-totalDistance/2)*(time*(time-2) - 1) + startValue;
	}

	//time and duration are in sec
	//
	float LinearTween(float time, float startValue, float totalDistance, float duration) {
		return totalDistance*time/duration + startValue;
	}


}
