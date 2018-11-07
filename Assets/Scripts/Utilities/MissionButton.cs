using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MissionButton : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler {
	public Sprite characterIcon, missionIcon;
	public string summaryText, detailsText;


	private Mission mission;
	

	public float hoverTime = 0.5f;
	public AnimationCurve hoverScaleCurve;

	private RectTransform hoverTransform;
	private IEnumerator hover;

	private TextMeshProUGUI summaryTMP, detailsTMP;

	// Use this for initialization
	void Start () {
		//SetValues(null, null, "", "");
		hoverTransform = transform.GetChild(0).GetComponent<RectTransform>();
		hoverTransform.sizeDelta = new Vector2(100,100);
		summaryTMP = transform.Find("Container/Information/Summary/Description/Text").GetComponent<TextMeshProUGUI>();
		Debug.Log(summaryTMP);
		detailsTMP = transform.Find("Container/Information/Details/Text").GetComponent<TextMeshProUGUI>();

	}
	
	// Update is called once per frame
	void Update () {
		summaryTMP.text = mission.ToString() + " " + summaryText;
	}

	public void OnPointerEnter(PointerEventData eventData) {//cancel whatever down scale coroutine is occuring, start upscale corouting
		if (hover != null) {
			StopCoroutine(hover);
		}
		HoverOpen();
    }
	public void OnPointerExit(PointerEventData eventData) {
		if (hover != null) {
			StopCoroutine(hover);
		}
		HoverClose();
    }

	private void HoverOpen() {
		hover = ScaleRectTransform(new Vector2(100,100), new Vector2(350,400));
		StartCoroutine(hover);
	}

	private void HoverClose() {
		hover = ScaleRectTransform(new Vector2(350,400), new Vector2(100,100));
		StartCoroutine(hover);
	}

	public IEnumerator ScaleRectTransform(Vector2 intendedStartSize, Vector2 endSize) {
		Vector2 actualStartSize = hoverTransform.sizeDelta;
		Vector2 sizeDelta = endSize-intendedStartSize;
		//200,200 - 100,100
		float percentComplete = (actualStartSize - intendedStartSize).magnitude/sizeDelta.magnitude;
		IEnumerator animationTime = AnimationUtilities.Animate(hoverScaleCurve, percentComplete, hoverTime, true);
		StartCoroutine(animationTime);

		while(animationTime.Current != null) {
			hoverTransform.sizeDelta = intendedStartSize + ((float)animationTime.Current*sizeDelta);
			yield return null;
		}
	}

	public void SetValues(Sprite character, Sprite missionSprite, string summary, string details, Mission mission) {
		GameObject characterGO = transform.Find("Container/Information/Summary/CharacterPanel/Icon").gameObject;
		GameObject missionGO = transform.Find("Container/Information/Summary/MissionPanel").gameObject;
		this.mission = mission;
		summaryText = summary;
		detailsText = details;

		characterGO.GetComponent<Image>().sprite = character;

		if (missionSprite == null) {
			missionGO.SetActive(false);
		} else {
			missionGO.transform.GetChild(0).GetComponent<Image>().sprite = missionSprite;
		}
	}
}
