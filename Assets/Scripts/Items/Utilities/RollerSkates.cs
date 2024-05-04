using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkates : Utility
{
    [SerializeField] private PlayerStats characterStats;
    public float percentageIncrease;

    public float GetMovementSpeedIncreasePercentage()
    {
        return percentageIncrease;
    }

    public void IncreaseMovementSpeed()
    {
        characterStats.MovementSpeedUpgrade(percentageIncrease/100);
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseMovementSpeed");
    }
}
