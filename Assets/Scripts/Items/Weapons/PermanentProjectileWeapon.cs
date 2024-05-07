using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PermanentProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;

    public int projectileRank;
    public int projectileUpgradeCost;

    public override void IncreaseDamage()
    {
        base.IncreaseDamage();
        foreach(Projectile projectile in gameObject.GetComponentsInChildren<Projectile>())
        {
            projectile.SetDamage(damage);
        }
    }

    public void IncreaseProjectileCount()
    {
        projectileRank++;
        amountOfProjectiles += projectileIncreasePerUpgrade;
    }

    public int GetProjectilePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseProjectileCount");
    }

    public abstract int GetIncreaseProjectileCountCost();
}
