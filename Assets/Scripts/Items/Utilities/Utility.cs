using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utility : Item
{
    public Player player;
    [SerializeField] protected float percentage;
    [SerializeField] protected float percentageIncreasePerUpgrade;


    public void UpgradeStat()
    {
        percentage += percentageIncreasePerUpgrade;
    }

    protected float GetStatPercentIncrease()
    {
        return percentage;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("UpgradeStat");
    }
}
