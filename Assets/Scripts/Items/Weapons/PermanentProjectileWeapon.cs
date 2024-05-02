using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PermanentProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;

    public int projectileRank;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < projectileRank; i++)
        {
            IncreaseProjectileCount();
        }
        EndInitialUpgrades();
    }

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
        amountOfProjectiles += projectileIncreasePerUpgrade;
        if (InitialUpgradesComplete())
        {
            ProjectileUpgradeRank(1);
        }
    }

    public void ProjectileUpgradeRank(int rankAmount)
    {
        projectileRank += rankAmount;
    }

    public int GetProjectileIncreasePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseProjectileCount");
    }
}
