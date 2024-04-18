using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfTargets;
    [SerializeField] protected int penetration;
    public void IncreaseTargetCount(int targetIncrease)
    {
        amountOfTargets += targetIncrease;
    }

    public void IncreasePenetrationAmount(int penetrationAmount)
    {
        penetration += penetrationAmount;
    }

}
