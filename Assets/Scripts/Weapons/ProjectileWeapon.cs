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

}
