using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utility : Item
{
    public Player player;
    [SerializeField] protected float percentage;
    [SerializeField] protected float percentageIncreasePerUpgrade;


    protected void UpgradeStat()
    {
        percentage += percentageIncreasePerUpgrade;
    }

    protected float GetStatPercentIncrease()
    {
        return percentage;
    }

    public override List<string> GetUpgradeOptions()
    {
        upgradeOptions.Add("UpgradeStat");
        return upgradeOptions;
    }
}
