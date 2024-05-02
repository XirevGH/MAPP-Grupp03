using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public float percentageIncrease;
    public int movementSpeedRank;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < movementSpeedRank; i++)
        {
            IncreaseMovementSpeed();
        }
        EndInitialUpgrades();
    }

    public float GetMovementSpeedIncreasePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        playerMovement.IncreaseMovementSpeed(1 + (percentageIncrease / 100f));
        if (InitialUpgradesComplete())
        {
            MovementSpeedUpgradeRank(1);
        }
    }
    
    public void MovementSpeedUpgradeRank(int rankAmount)
    {
        movementSpeedRank += rankAmount;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseMovementSpeed");
    }
}
