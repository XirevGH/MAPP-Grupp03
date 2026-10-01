using UnityEngine;

public abstract class Utility : Item
{
    [SerializeField] protected UtilityDataSO utilityData;
    public override ItemDefinitionSO BaseItemData => utilityData;
}
