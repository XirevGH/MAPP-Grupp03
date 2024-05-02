using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveArmor : Utility
{
    
    public float percentageIncrease;
    public int healthRank;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < healthRank; i++)
        {
            IncreaseHealth();
        }
        EndInitialUpgrades();
    }

    public void IncreaseHealth() 
    { 
        player.IncreaseMaxHealth(1 + (percentageIncrease / 100f));
        if (InitialUpgradesComplete())
        {
            HealthUpgradeRank(1);
        }
    }

    public void HealthUpgradeRank(int rankAmount)
    {
        healthRank += rankAmount;
    }

    public float GetHealthIncreasePercentage()
    {
        return percentageIncrease;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseHealth");
    }
}
