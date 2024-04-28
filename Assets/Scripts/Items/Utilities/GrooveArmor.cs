using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrooveArmor : Utility
{
    public float percentageIncrease;
    public void IncreaseHealth() 
    { 
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
}
