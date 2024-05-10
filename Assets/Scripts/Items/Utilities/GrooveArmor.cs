using System;
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

    public float GetHealthIncreasePercentage()
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

    public float GetCurrentHealthIncrease()
    {
        return (float)Math.Round((Mathf.Pow(1 + (percentageIncrease / 100f), healthRank) - 1) * 100, 1); ;
    }
}
