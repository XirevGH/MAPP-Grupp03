
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;
    public int damageRank;
    public float percentageDamageIncrease;

    public abstract void Attack();

    public void DamageUpgradeRank(int rankIncrease)
    {
        damageRank += rankIncrease;
        if (!InitialUpgradesComplete())
        {
            Debug.Log(damageRank);
            for (int i = 0; i < damageRank; i++) 
            {
                IncreaseDamage();
            }
            EndInitialUpgrades();
        }
    }

    public virtual void IncreaseDamage()
    {
        damage *= (1 + (percentageDamageIncrease / 100f));
        if (InitialUpgradesComplete())
        {
            DamageUpgradeRank(1);
        }
    }

    public float GetDamageIncreasePercentage()
    {
        return percentageDamageIncrease;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
    }
}
