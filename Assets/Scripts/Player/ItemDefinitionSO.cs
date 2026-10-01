using System.Collections.Generic;
using UnityEngine;

public abstract class ItemDefinitionSO : ScriptableObject
{
    [Header("Base Identity")]
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [TextArea][SerializeField] private string baseDescription;

    [Header("Rhythm Configuration")]
    [Tooltip("Set to 0 if this item is a passive stat. Set to > 0 for beat triggers.")]
    [SerializeField] private int beatNumber = 1;

    [Header("Meta Upgrades")]
    [SerializeField] private List<UpgradeDefinitionSO> availableMetaUpgrades;

    // Public Getters & Helpers
    public string ItemID => itemID;
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public string BaseDescription => baseDescription;
    public int BeatNumber => beatNumber;
    public bool HasBeatTrigger => beatNumber > 0;
    public IReadOnlyList<UpgradeDefinitionSO> AvailableMetaUpgrades => availableMetaUpgrades;

    public UpgradeDefinitionSO GetUpgrade(StatType statType)
    {
        if (availableMetaUpgrades == null) return null;
        return availableMetaUpgrades.Find(u => u.TargetStat == statType);
    }

    public abstract string GetItemType();
}