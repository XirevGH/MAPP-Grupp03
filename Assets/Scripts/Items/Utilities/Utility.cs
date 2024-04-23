using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : Item
{
    public Player player;
    protected float percentageIncrease;

    private void Start()
    {
        percentageIncrease = 1.1f;
    }

    protected void UpgradeStat(float amount)
    {
        percentageIncrease += amount;
    }


    protected float GetStatPercentIncrease()
    {
        return percentageIncrease;
    }

    public override List<string> GetUpgradeOptions()
    {
        throw new System.NotImplementedException();
    }
}
