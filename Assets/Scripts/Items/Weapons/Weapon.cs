
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;
    public int damageRank;
    public float percentageDamageIncrease;

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
}
