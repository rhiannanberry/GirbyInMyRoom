using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mission : Condition {
	public GameObject missionDetailsPrefab;
	public Sprite missionIcon, bugIcon, achievementIcon;
	public string summary, details;

	private List<Transform> items;
	private int itemsCount;
	private int startItemsCount;


	public int ItemsCount {
		get { return itemsCount; }
	}

	public int StartItemsCount {
		get { return startItemsCount; }
	}

	// Use this for initialization
	void Start () {
		items = (new List<Transform>(transform.GetComponentsInChildren<Transform>()));
		items.RemoveAt(0); 
		itemsCount = items.Count;
		startItemsCount = itemsCount;
		missionDetailsPrefab = Prefabs.missionDetailsPrefab.gameObject;
		Debug.Log(missionDetailsPrefab);
		
		GameObject mList = GetComponentInParent<MissionsContainer>().missionsUIContainer;
		//mList.SetActive(true);
		GameObject m = (GameObject)Instantiate(missionDetailsPrefab, Vector3.zero, Quaternion.identity, mList.transform);
		//mList.SetActive(false);
		m.GetComponent<MissionButton>().SetValues(bugIcon, missionIcon,summary,details, GetComponent<Mission>());
	}
	
	// Update is called once per frame
	void Update () {
		Transform[] activeChildren = transform.GetComponentsInChildren<Transform>();
		if (activeChildren.Length-1 != itemsCount) {
			items = new List<Transform>(activeChildren); 
			items.RemoveAt(0);
			itemsCount = items.Count;
		}
		base.conditionMet = itemsCount == 0;
	}

	public override string ToString() {
		return (startItemsCount - itemsCount).ToString() + "/" + startItemsCount.ToString();
	}
	public string ToStringDelay() {
		return (startItemsCount - itemsCount+1).ToString() + "/" + startItemsCount.ToString();
	}
}
