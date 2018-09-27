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
			StartCoroutine(MoveMissionPanel(new Vector2(0, 100), 0.4f, 2f, 0.3f));
			HUDActions.updateMission = false;
		}
	}

	IEnumerator MoveMissionPanel(Vector2 delta, float inTime, float holdTime, float outTime) {
		//show panel
		yield return StartCoroutine(AnimationUtilities.MoveUI(missionPanel, delta, inTime, AnimationUtilities.CurveType.EaseOut));
		
		//hold panel
		yield return new WaitForSeconds(holdTime);
		
		//hide panel
		yield return StartCoroutine(AnimationUtilities.MoveUI(missionPanel, -delta, outTime, AnimationUtilities.CurveType.EaseIn));

	}

	float EaseOut(float time, float startValue, float totalDistance, float duration) {
		time /= duration;
		return (-totalDistance)*time*(time-2) + startValue;
	}
}
