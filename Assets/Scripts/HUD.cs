using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class HUDActions {
	public static Mission mission;
	public static bool updateMission = false;

	public static void UpdateMission(Mission newMission) {
		mission = newMission;
		updateMission = true;
	}
}


public class HUD : MonoBehaviour {

	public RectTransform missionPanel;
	TextMeshProUGUI missionItemCount;
	Image missionSprite;

	// Use this for initialization
	void Start () {
		missionItemCount = missionPanel.GetComponentInChildren<TextMeshProUGUI>();
		missionSprite = missionPanel.Find("ItemImage/Mask/Image").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (HUDActions.updateMission) {
			missionItemCount.text = HUDActions.mission.ToString();
			missionSprite.sprite = HUDActions.mission.missionIcon;
			StartCoroutine(AnimationUtilities.MoveUI(missionPanel, new Vector2(-125, -120), new Vector2(0, 100), 0.5f, AnimationUtilities.CurveType.EaseOut));
			HUDActions.updateMission = false;
		}
	}

	IEnumerator MoveHUDPanel(RectTransform rect, Vector2 delta) {
		float dist = delta.magnitude;
		Vector2 dir = delta.normalized;
		rect.anchoredPosition+=new Vector2(0, -100);
		Vector2 startPos = rect.anchoredPosition;
		float currentTime = 0.0f;
		float currentDist = 0.0f;
		float duration = 0.5f;
		while( currentTime <= duration) {
			currentTime += Time.deltaTime;
			currentDist = EaseOut(currentTime, 0, dist, duration);
			if (currentDist >= dist) {
				rect.anchoredPosition = startPos + dist*dir;
			} else {
				rect.anchoredPosition = startPos + currentDist*dir;
			}
			yield return new WaitForFixedUpdate();
		}
	}

	float EaseOut(float time, float startValue, float totalDistance, float duration) {
		time /= duration;
		return (-totalDistance)*time*(time-2) + startValue;
	}
}
