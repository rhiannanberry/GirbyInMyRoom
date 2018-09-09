using UnityEngine;

public class ComparisionCondition : Condition
{
    public int matchTo;

    public int currentValue;

    new public bool IsSatisfied() {
        return (matchTo == currentValue);
    }
}