using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    public PlayerMovement playerMovement;
    public float percentageIncrease;

    public float GetMovementSpeedIncreasePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        playerMovement.IncreaseMovementSpeed(1 + (percentageIncrease / 100f));
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseMovementSpeed");
    }
}
