using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mission : Condition {

	private List<Transform> items;
	private int itemsCount;


	public int ItemsCount {
		get { return itemsCount; }
	}

	// Use this for initialization
	void Start () {
		items = (new List<Transform>(transform.GetComponentsInChildren<Transform>()));
		items.RemoveAt(0); 
		itemsCount = items.Count;
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
}
