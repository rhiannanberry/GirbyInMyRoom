using UnityEngine;


[System.Serializable]
public class BoolCondition : Condition
{
    public override bool IsSatisfied() {
        return conditionToMatch == currentCondition;
    }
}