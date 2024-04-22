using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PenetratingProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int penetration;

    public void IncreaseTargetCount(int targetIncrease)
    {
        amountOfProjectiles += targetIncrease;
    }

    public void IncreasePenetrationAmount(int penetrationAmount)
    {
        penetration += penetrationAmount;
    }

}
