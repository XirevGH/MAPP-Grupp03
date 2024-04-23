using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int penetration;

    public void IncreaseProjectileCount(int projectileIncrease)
    {
        amountOfProjectiles += projectileIncrease;
    }

    public void IncreasePenetrationAmount(int penetrationAmount)
    {
        penetration += penetrationAmount;
    }

    public override List<string> GetUpgradeOptions()
    {
        for (int i = 0; i < GetUpgradeOptions().Count; i++)
        {
            upgradeOptions.Add(base.GetUpgradeOptions()[i]);
        }
        upgradeOptions.Add("IncreaseProjectileCount");
        upgradeOptions.Add("IncreasePenetrationAmount");
        return upgradeOptions;
    }
}
