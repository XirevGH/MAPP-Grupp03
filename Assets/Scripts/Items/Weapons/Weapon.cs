using System.Collections.Generic;
using System;

public abstract class Weapon : Item
{
    public float damage;
    public float percentageDamageIncreasePerUpgrade;

    public abstract void Attack();

    public void IncreaseDamage()
    {
        damage *= percentageDamageIncreasePerUpgrade;
    }

    public override List<string> GetUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
        return upgradeOptions;
    }
}
