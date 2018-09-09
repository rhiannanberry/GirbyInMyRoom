using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Interaction {

	[SerializeField]
	public BoolCondition boolCondition;

	public IntCondition compCondition;

	public UnityEvent action;


	public bool CheckAndReact() {
		bool condNotNull = (boolCondition != null);
		bool compNotNull = (compCondition != null);
		bool condResult = !condNotNull;
		bool compResult = !compNotNull;
		if (!compNotNull && !condNotNull) {
			Debug.Log("This interaction doesn't contain any condition, so it will trigger successful. ");
		} else {
			if (condNotNull) {
				condResult = boolCondition.IsSatisfied();
			}
			if (compNotNull) {
				compResult = compCondition.IsSatisfied();
			}
		}

		if (condResult && compResult) {
			action.Invoke();
			return true;
		}

		return false;
	}
}

[System.Serializable]
public class InteractionNonSequenced : Interaction {
	public bool repeat = true;
	private int timesRepeated = 0;

	new public bool CheckAndReact() {
		bool condNotNull = (boolCondition != null);
		bool compNotNull = (compCondition != null);
		bool condResult = !condNotNull;
		bool compResult = !compNotNull;
		if (!compNotNull && !condNotNull) {
			Debug.Log("This interaction doesn't contain any condition, so it will trigger successful. ");
		} else {
			if (condNotNull) {
				condResult = boolCondition.IsSatisfied();
			}
			if (compNotNull) {
				compResult = compCondition.IsSatisfied();
			}
		}

		Debug.Log(condResult);

		if (condResult && compResult && (repeat || (timesRepeated<1))) {
			action.Invoke();
			timesRepeated++;
			return true;
		}

		return false;
	}

}
