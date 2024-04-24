using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PermanentProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;

    public void IncreaseProjectileCount(int projectileIncrease)
    {
        amountOfProjectiles += projectileIncrease;
    }

    public override List<string> GetUpgradeOptions()
    {
        base.GetUpgradeOptions();
        upgradeOptions.Add("IncreaseProjectileCount");
        return upgradeOptions;
    }
}
