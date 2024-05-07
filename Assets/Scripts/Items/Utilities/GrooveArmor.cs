using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveArmor : Utility
{
    public float percentageIncrease;
    public int healthRank;
    public int healthUpgradeCost;

    public void IncreaseHealth() 
    {
        healthRank++;
        player.IncreaseMaxHealth(1 + (percentageIncrease / 100f));
    }

    public float GetHealthUpgradePercentage()
    {
        return percentageIncrease;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseHealth");
    }

    public int GetIncreaseHealthCost()
    {
        return healthUpgradeCost;
    }
}
