using UnityEngine;

[System.Serializable]
public class IntCondition : Condition
{
    public int matchTo;

    public int currentValue;

    public override bool IsSatisfied() {
        currentCondition = (matchTo == currentValue);
        return currentCondition==conditionToMatch;
    }

    public void AddToCurrent(int value) {
        currentValue += value;
    }
}