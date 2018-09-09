using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Condition", menuName = "Conditions/Condition", order = 1)]
public class Condition : ScriptableObject
{
    public string description;
    public bool satisfied;

    public bool IsSatisfied() {
        return satisfied;
    }
}