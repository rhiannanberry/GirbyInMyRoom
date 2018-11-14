using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEvent : Condition  {
	public string description;

	public void SetConditionValue(bool val) {
		base.conditionMet = val;
	}

	public bool conditionMet {
		get { return conditionMet; }
	}
	
}
