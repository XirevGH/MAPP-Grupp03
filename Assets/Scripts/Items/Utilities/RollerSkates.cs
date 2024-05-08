using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public float percentageIncrease;
    public int movementSpeedRank;
    public int movementSpeedUpgradeCost;

    public float GetMovementSpeedIncreasePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        movementSpeedRank++;
        playerMovement.IncreaseMovementSpeed(1 + (percentageIncrease / 100f));
    }
    
    public float GetCurrentMovementSpeedIncrease()
    {
        return Mathf.Pow(1 + (percentageIncrease / 100f), movementSpeedRank);
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseMovementSpeed");
    }

    public int GetIncreaseMovementSpeedCost()
    {
        return movementSpeedUpgradeCost;
    }
}
