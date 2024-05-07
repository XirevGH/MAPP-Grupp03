using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public float percentageIncrease;
    public int movementSpeedRank;
    public int movementSpeedUpgradeCost;

    public float GetMovementSpeedUpgradePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        movementSpeedRank++;
        playerMovement.IncreaseMovementSpeed(1 + (percentageIncrease / 100f));
    }
    
    protected override void CreateUpgradeOptions()
    {
        Debug.Log("Created upgrade options for roller skates");
        upgradeOptions.Add("IncreaseMovementSpeed");
    }

    public int GetIncreaseMovementSpeedCost()
    {
        return movementSpeedUpgradeCost;
    }
}
