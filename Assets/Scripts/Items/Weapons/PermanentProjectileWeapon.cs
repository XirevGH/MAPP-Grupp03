using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PermanentProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;
    public void IncreaseProjectileCount()
    {
        amountOfProjectiles += projectileIncreasePerUpgrade;
    }

    public override List<string> GetUpgradeOptions()
    {
        base.GetUpgradeOptions();
        upgradeOptions.Add("IncreaseProjectileCount");
        return upgradeOptions;
    }
}
