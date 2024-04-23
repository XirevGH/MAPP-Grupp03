using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;

    public abstract void Attack();

    public void IncreaseDamage(float percentageAmount)
    {
        damage *= percentageAmount;
    }

    public override List<string> GetUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
        return upgradeOptions;
    }
}
