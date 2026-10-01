using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrooveArmor : Utility
{
    private bool isApplied = false;

    private void Start()
    {
        ApplyMetaHealthBonus();
    }

    private void ApplyMetaHealthBonus()
    {
        if (isApplied) return;
        if (utilityData == null || player == null) return;

        // 1. Look up the Health meta-upgrade
        var healthUpgrade = utilityData.GetUpgrade(StatType.Health);

        if (healthUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            // Gets base value (e.g., 15%) + meta rank bonuses (e.g., +15%) = 30%
            float totalBonusPercent = MetaUpgradeManager.Instance.GetStatValue(healthUpgrade);

            if (totalBonusPercent > 0)
            {
                // Multiplier: 1 + (30 / 100) = 1.30x
                player.IncreaseMaxHealth(1f + (totalBonusPercent / 100f));
            }
        }

        isApplied = true;
    }

    public void IncreaseInRunHealth(float percentage)
    {
        if (player != null)
        {
            player.IncreaseMaxHealth(1f + (percentage / 100f));
        }
    }
}
