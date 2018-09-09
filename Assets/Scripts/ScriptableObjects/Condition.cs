using UnityEngine;


[System.Serializable]
public abstract class Condition : ScriptableObject
{
    public string description;
    public bool conditionToMatch;
    public bool currentCondition;

    abstract public bool IsSatisfied();
}