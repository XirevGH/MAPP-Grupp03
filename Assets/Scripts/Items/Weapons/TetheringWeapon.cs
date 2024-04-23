using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TetheringWeapon : Weapon
{
    [SerializeField] protected int amountOfTethers;

    public void UpgradeTetherAmount(int amount)
    {
        amountOfTethers += amount;
    }
}
