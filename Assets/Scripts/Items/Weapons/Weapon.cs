using System;
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;
    public int damageRank;
    public int damageUpgradeCost;
    public float percentageDamageIncrease;
    public AudioClip attackSound;

    protected override void Awake()
    {
        base.Awake();
    }

    public abstract void Attack();

    public virtual void IncreaseDamage()
    {
        damageRank++;
        damage *= (1 + (percentageDamageIncrease / 100f));
    }

    public float GetDamageIncreasePercentage()
    {
        return percentageDamageIncrease;
    }

    public float GetCurrentDamage()
    {
        return (float)Math.Round(damage, 1);
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
    }

    public override string GetItemType()
    {
        return "Weapon";
    }

    public int GetIncreaseDamageCost()
    {
        return damageUpgradeCost;
    }

    public int GetDamageUpgradeRank()
    {
        return damageRank;
    }
}
