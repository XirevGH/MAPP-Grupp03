using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    protected int metaUpgradedProjectiles;
    protected int metaUpgradedPenetration;

    protected int inRunBonusProjectiles = 0;
    protected int inRunBonusPenetration = 0;

    protected override void Start()
    {
        base.Start();
        InitializeProjectileStats();
    }
    public virtual void InitializeProjectileStats()
    {
        if (weaponData == null) return;

        var projUpgrade = weaponData.GetUpgrade(StatType.ProjectileCount);
        if (projUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            metaUpgradedProjectiles = Mathf.RoundToInt(MetaUpgradeManager.Instance.GetStatValue(projUpgrade));
        }
        else
        {
            metaUpgradedProjectiles = 1;
        }

        var penUpgrade = weaponData.GetUpgrade(StatType.Penetration);
        if (penUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            metaUpgradedPenetration = Mathf.RoundToInt(MetaUpgradeManager.Instance.GetStatValue(penUpgrade));
        }
        else
        {
            metaUpgradedPenetration = 1;
        }
    }

    public int GetCurrentProjectileCount()
    {
        return metaUpgradedProjectiles + inRunBonusProjectiles;
    }

    public int GetCurrentPenetration()
    {
        return metaUpgradedPenetration + inRunBonusPenetration;
    }

    public void IncreaseInRunProjectiles(int amount)
    {
        inRunBonusProjectiles += amount;
    }

    public void IncreaseInRunPenetration(int amount)
    {
        inRunBonusPenetration += amount;
    }
}
