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

        var healthUpgrade = utilityData.GetUpgrade(StatType.Health);

        if (healthUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            float totalBonusPercent = MetaUpgradeManager.Instance.GetStatValue(healthUpgrade);

            if (totalBonusPercent > 0)
            {
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
