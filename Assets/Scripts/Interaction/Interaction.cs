using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Interaction {

	[SerializeField]
	public Condition condition;

	public ComparisionCondition compCondition;

	public UnityEvent action;

	public void CheckAndReact() {
		bool condNotNull = (condition != null);
		bool compNotNull = (compCondition != null);
		bool condResult = !condNotNull;
		bool compResult = !compNotNull;
		if (!compNotNull && !condNotNull) {
			Debug.LogError("Interaction does not contain condition or comparison condition");
		} else {
			if (condNotNull) {
				condResult = condition.IsSatisfied();
			}
			if (compNotNull) {
				compResult = compCondition.IsSatisfied();
			}
		}

		Debug.Log(condResult);

		if (condResult && compResult) {
			action.Invoke();
		}
	}

}
