using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TetheringWeapon : Weapon
{
    [SerializeField] protected int amountOfTethers;
    [SerializeField] protected int tetherIncreasePerUpgrade;
    protected HashSet<GameObject> enemies = new HashSet<GameObject>();
    public int tetherRank;
    public int tetherUpgradeCost;

    public void IncreaseTetherAmount()
    {
        tetherRank++;
        amountOfTethers += tetherIncreasePerUpgrade;
    }

    protected GameObject[] GetClosestEnemies(int amountOfTethers)
    {
        SortedSet<GameObject> sortedEnemies = new SortedSet<GameObject>(new GameObjectComparer());
        foreach (GameObject enemy in enemies)
        {
            sortedEnemies.Add(enemy);
        }
        return sortedEnemies.Take(amountOfTethers).ToArray();
    }
    protected int AdjustTargetOverflow(int amountOfTethers)
    {
        if (enemies.Count < amountOfTethers)
        {
            return enemies.Count;
        }
        else
        {
            return amountOfTethers;
        }
    }

    public int GetTetherAmountPerUpgrade()
    {
        return tetherIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseTetherAmount");
    }

    public abstract int GetIncreaseTetherAmountCost();
}
