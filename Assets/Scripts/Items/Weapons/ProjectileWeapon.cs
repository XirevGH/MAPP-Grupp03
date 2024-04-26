using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;
    [SerializeField] protected int penetration;
    [SerializeField] protected int penetrationIncreaserPerUpgrade;

    public void IncreaseProjectileCount()
    {
        amountOfProjectiles += projectileIncreasePerUpgrade;
    }

    public void IncreasePenetrationAmount()
    {
        penetration += penetrationIncreaserPerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();

        upgradeOptions.Add("IncreaseProjectileCount");
        upgradeOptions.Add("IncreasePenetrationAmount");
    }
}
