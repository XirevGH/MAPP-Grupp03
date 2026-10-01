using UnityEngine;

[CreateAssetMenu(fileName = "NewUtilityData", menuName = "Game/Items/Utility Data")]
public class UtilityDataSO : ItemDefinitionSO
{
    public override string GetItemType() => "Utility";
}