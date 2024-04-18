using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAbility : MonoBehaviour
{
    public Weapon[] weapons;

    public void UpgradeDamage(Weapon weapon, float percentageIncrease)
    {
        weapon.IncreaseDamage(percentageIncrease);
    }
    public void UpgradeTargetCount(ProjectileWeapon weapon, int targetIncrease)
    {
        weapon.IncreaseTargetCount(targetIncrease);
    }

    public void UpgradePenetrationAmount(ProjectileWeapon weapon, int penetrationAmount)
    {
        weapon.IncreasePenetrationAmount(penetrationAmount);
    }
}
