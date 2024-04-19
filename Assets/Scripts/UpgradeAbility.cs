using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAbility : MonoBehaviour
{
    public Weapon[] weapons;
    List<Weapon> weaponList;

    void Start()
    {
        weaponList = new List<Weapon>(weapons);
    }

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

    public Weapon ChooseRandomWeapon()
    {
        int randomWeapon = Random.Range(0, weaponList.Count);
        return weaponList[randomWeapon];
    }

    public List<Weapon> GetAvailableWeapons()
    {
        return new List<Weapon>(weaponList);
    }

    public void ChooseWeapon(Weapon weapon)
    {
        weaponList.Remove(weapon);
    }
}
