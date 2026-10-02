using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization; // ◄── Required for Unity Localization

public abstract class ItemDefinitionSO : ScriptableObject
{
    [Header("Identity & Localization")]
    [SerializeField] private string itemID;
    [SerializeField] private LocalizedString localizedItemName;       // e.g. StringTable / item_bass_name
    [SerializeField] private LocalizedString localizedDescription;    // e.g. StringTable / item_bass_desc
    [SerializeField] private Sprite icon;

    // Fallbacks
    [SerializeField] private string fallbackItemName;
    [TextArea][SerializeField] private string fallbackDescription;

    [Header("Rhythm Configuration")]
    [Tooltip("Set to 0 if this item is a passive stat (e.g., Roller Skates). Set to > 0 for beat triggers (e.g. 1, 2, 4).")]
    [SerializeField] private int beatNumber = 1;

    [Header("Meta Upgrades")]
    [SerializeField] private List<UpgradeDefinitionSO> availableMetaUpgrades;

    // Public Getters
    public string ItemID => itemID;
    public Sprite Icon => icon;
    public int BeatNumber => beatNumber;
    public bool HasBeatTrigger => beatNumber > 0;
    public IReadOnlyList<UpgradeDefinitionSO> AvailableMetaUpgrades => availableMetaUpgrades;

    // Localized Name (Returns translated string based on active locale)
    public string ItemName
    {
        get
        {
            if (localizedItemName != null && !localizedItemName.IsEmpty)
            {
                return localizedItemName.GetLocalizedString();
            }
            return !string.IsNullOrEmpty(fallbackItemName) ? fallbackItemName : name;
        }
    }

    // Localized Base Description
    public string BaseDescription
    {
        get
        {
            if (localizedDescription != null && !localizedDescription.IsEmpty)
            {
                return localizedDescription.GetLocalizedString();
            }
            return fallbackDescription;
        }
    }

    public UpgradeDefinitionSO GetUpgrade(StatType statType)
    {
        if (availableMetaUpgrades == null) return null;
        return availableMetaUpgrades.Find(u => u.TargetStat == statType);
    }

    public abstract string GetItemType();
}