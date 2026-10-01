using UnityEngine;

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
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [TextArea][SerializeField] private string descriptionFormat;

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
    public string DisplayName => displayName;
    public Sprite Icon => icon;
    public StatType TargetStat => targetStat;
    public int MaxRank => maxRank;
    public float IncreasePerRank => increasePerRank;
    public bool IsPercentage => isPercentage;
    public float BaseValue => baseValue;

    public int GetCostForRank(int currentRank)
    {
        if (currentRank >= maxRank) return int.MaxValue;
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplierPerRank, currentRank));
    }

    public float GetCalculatedValue(int currentRank)
    {
        return baseValue + (currentRank * increasePerRank);
    }

    public string GetFormattedDescription(int currentRank)
    {
        float value = GetCalculatedValue(currentRank);
        string formattedValue = isPercentage ? $"{value:0.#}%" : $"{value:0.#}";
        return string.Format(descriptionFormat, formattedValue);
    }
}