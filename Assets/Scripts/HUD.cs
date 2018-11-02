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

	public AnimationCurve UIWobble, UISlideIn;

	public RectTransform missionPanel;
	TextMeshProUGUI missionItemCount;
	Vector2 missionPanelStartPosition;
	Image missionSprite;

	private List<IEnumerator> missionPanelCoroutines = new List<IEnumerator>();
	// Use this for initialization
	void Start () {
		missionItemCount = missionPanel.GetComponentInChildren<TextMeshProUGUI>();
		missionSprite = missionPanel.Find("ItemImage/Mask/Image").GetComponent<Image>();
		missionPanelStartPosition = missionPanel.anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (HUDActions.updateMission) {
			missionItemCount.text = HUDActions.mission.ToString();
			missionSprite.sprite = HUDActions.mission.missionIcon;
			if (missionPanelCoroutines.Count > 0) {
				foreach (IEnumerator cor in missionPanelCoroutines) {
					StopCoroutine(cor);
				}
				missionPanelCoroutines.Clear();
			}
			IEnumerator mainCor = MoveMissionPanel(new Vector2(0, 100), .5f, 2f, .4f);
			StartCoroutine(mainCor);
			missionPanelCoroutines.Add(mainCor);
			//StartCoroutine(AnimationUtilities.ScaleWobble(missionPanel, new Vector2(0.01f, 0.01f), 1.5f, 4));

			HUDActions.updateMission = false;
		}
	}

	IEnumerator MoveMissionPanel(Vector2 delta, float inTime, float holdTime, float outTime) {
		Vector2 deltaOffset =  missionPanel.anchoredPosition - missionPanelStartPosition;
		Vector2 newDelta = delta - deltaOffset;
		
		float newInTime = (newDelta.magnitude/delta.magnitude)*inTime;

		Debug.Log("deltaOffset: " + deltaOffset + " newDelta: " + newDelta + " newInTime: " + newInTime);
		IEnumerator showPanel = AnimationUtilities.MoveUI(missionPanel, newDelta, UISlideIn, newInTime, true);
		IEnumerator hidePanel = AnimationUtilities.MoveUI(missionPanel, -delta, UISlideIn, outTime, true);
		missionPanelCoroutines.Add(showPanel);

		while(States.paused) {
			yield return null;
		}

		//show panel
		yield return StartCoroutine(showPanel);
		missionPanel.anchoredPosition = missionPanelStartPosition + delta;
		StartCoroutine(AnimationUtilities.ScaleWobble(missionPanel, new Vector2(-.01f, .02f), UIWobble, holdTime/2, true));

		while(States.paused) {
			yield return null;
		}

		//hold panel
		yield return new WaitForSeconds(holdTime);

		missionPanelCoroutines.Add(hidePanel);
		//hide panel
		yield return StartCoroutine(hidePanel);
		missionPanel.anchoredPosition = missionPanelStartPosition;

	}

	
}
