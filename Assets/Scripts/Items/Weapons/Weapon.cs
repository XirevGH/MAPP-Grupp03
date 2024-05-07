
using UnityEngine;

public abstract class Weapon : Item
{
    protected int damage;
    public float baseDamage;
    public int damageRank;
    public int damageUpgradeCost;
    public float percentageDamageIncrease;
    public AudioClip attackSound;

    protected override void Awake()
    {
        base.Awake();
        damage = (int)baseDamage;
    }

    public abstract void Attack();

    public virtual void IncreaseDamage()
    {
        damageRank++;
        baseDamage *= (1 + (percentageDamageIncrease / 100f));
        damage = (int) baseDamage;
    }

    public float GetDamageIncreasePercentage()
    {
        return percentageDamageIncrease;
    }

    public int GetCurrentDamage()
    {
        return damage;
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
