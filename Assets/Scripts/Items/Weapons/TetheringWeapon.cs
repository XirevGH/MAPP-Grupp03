using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TetheringWeapon : Weapon
{
    protected int metaUpgradedTethers;
    protected int inRunBonusTethers = 0;

    protected HashSet<GameObject> enemies = new HashSet<GameObject>();

    protected override void Start()
    {
        base.Start();
        InitializeTetherStats();
    }

    public virtual void InitializeTetherStats()
    {
        if (weaponData == null) return;

        var tetherUpgrade = weaponData.GetUpgrade(StatType.TetherAmount);

        if (tetherUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            metaUpgradedTethers = Mathf.RoundToInt(MetaUpgradeManager.Instance.GetStatValue(tetherUpgrade));
        }
        else
        {
            metaUpgradedTethers = 1;
        }
    } 

    protected GameObject[] GetClosestEnemies(int targetCount)
    {
        SortedSet<GameObject> sortedEnemies = new SortedSet<GameObject>(new GameObjectComparer());
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                sortedEnemies.Add(enemy);
            }
        }
        return sortedEnemies.Take(targetCount).ToArray();
    }

    protected int AdjustTargetOverflow(int targetCount)
    {
        return Mathf.Min(enemies.Count, targetCount);
    }

    public void IncreaseInRunTethers(int amount)
    {
        inRunBonusTethers += amount;
    }

    public int GetCurrentTetherAmount()
    {
        return metaUpgradedTethers + inRunBonusTethers;
    }
}
