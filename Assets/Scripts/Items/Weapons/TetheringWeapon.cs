using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TetheringWeapon : Weapon
{
    [SerializeField] protected int amountOfTethers;
    protected HashSet<GameObject> enemies = new HashSet<GameObject>();

    public void UpgradeTetherAmount(int amount)
    {
        amountOfTethers += amount;
    }

    protected GameObject[] GetClosestEnemies(int amountOfTargets)
    {
        SortedSet<GameObject> sortedEnemies = new SortedSet<GameObject>(new GameObjectComparer());
        foreach (GameObject enemy in enemies)
        {
            sortedEnemies.Add(enemy);
        }
        return sortedEnemies.Take(amountOfTargets).ToArray();
    }
    protected int AdjustTargetOverflow(int amountOfTargets)
    {
        if (enemies.Count < amountOfTargets)
        {
            return enemies.Count;
        }
        else
        {
            return amountOfTargets;
        }
    }

    public override List<string> GetUpgradeOptions()
    {
        base.GetUpgradeOptions();
        upgradeOptions.Add("IncreaseTetherAmount");
        return upgradeOptions;
    }
}
