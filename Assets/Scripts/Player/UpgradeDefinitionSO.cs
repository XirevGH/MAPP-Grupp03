using UnityEngine;
using UnityEngine.Localization;

public enum StatType
{
    Damage,
    ProjectileCount,
    Penetration,
    Radius,
    TetherAmount,
    MovementSpeed,
    Health,
    SlowPercentage,
    Force,
    DecoyAmount,
    DecoyHealth
}

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Meta Progression/Upgrade Definition")]
public class UpgradeDefinitionSO : ScriptableObject
{
    [Header("Identity")]
    [SerializeField] private string upgradeID;
    [SerializeField] private LocalizedString localizedDisplayName;
    [SerializeField] private LocalizedString localizedDescription;
    [SerializeField] private Sprite icon;

    [SerializeField] private string fallbackDisplayName = "Upgrade";
    [SerializeField] private string fallbackDescriptionFormat = "+{0}";

    [Header("Stat Configuration")]
    [SerializeField] private StatType targetStat;
    [SerializeField] private float baseValue = 0f;
    [SerializeField] private float increasePerRank = 5f;
    [SerializeField] private bool isPercentage = true;

    [Header("Economy & Progression")]
    [SerializeField] private int maxRank = 10;
    [SerializeField] private int baseCost = 100;
    [SerializeField] private float costMultiplierPerRank = 1.35f;

    public string UpgradeID => upgradeID;
    public Sprite Icon => icon;
    public StatType TargetStat => targetStat;
    public int MaxRank => maxRank;
    public float IncreasePerRank => increasePerRank;
    public bool IsPercentage => isPercentage;
    public float BaseValue => baseValue;

    public string DisplayName
    {
        get
        {
            if (localizedDisplayName != null && !localizedDisplayName.IsEmpty)
            {
                return localizedDisplayName.GetLocalizedString();
            }
            return !string.IsNullOrEmpty(fallbackDisplayName) ? fallbackDisplayName : name;
        }
    }

    public string GetFormattedDescription(float boostAmount)
    {
        string formattedValue = isPercentage ? $"{boostAmount:0.#}%" : $"{boostAmount:0.#}";

        if (localizedDescription != null && !localizedDescription.IsEmpty)
        {
            return localizedDescription.GetLocalizedString(formattedValue);
        }

        if (!string.IsNullOrEmpty(fallbackDescriptionFormat))
        {
            return string.Format(fallbackDescriptionFormat, formattedValue);
        }

        return isPercentage ? $"+{formattedValue}" : $"+{boostAmount:0.#}";
    }

    public string GetFormattedDescription(int currentRank)
    {
        float totalValue = GetCalculatedValue(currentRank);
        return GetFormattedDescription(totalValue);
    }

    public int GetCostForRank(int currentRank)
    {
        if (currentRank >= maxRank) return int.MaxValue;
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplierPerRank, currentRank));
    }

    public float GetCalculatedValue(int currentRank)
    {
        return baseValue + (currentRank * increasePerRank);
    }
}