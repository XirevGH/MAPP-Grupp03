
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;
    public int damageRank;
    public int damageUpgradeCost;
    public float percentageDamageIncrease;
    public AudioClip attackSound;

    public abstract void Attack();

    public virtual void IncreaseDamage()
    {
        damageRank++;
        damage *= (1 + (percentageDamageIncrease / 100f));
    }

    public float GetDamageUpgradePercentage()
    {
        return percentageDamageIncrease;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
    }

    public override string GetItemType()
    {
        return "Weapon";
    }

    public abstract int GetIncreaseDamageCost();
}
