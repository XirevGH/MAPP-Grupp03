using UnityEngine;

public abstract class PermanentProjectileWeapon : Weapon
{
    protected int metaUpgradedProjectiles;
    protected int inRunBonusProjectiles = 0;

    protected override void Start()
    {
        base.Start();
        InitializePermanentProjectileStats();
    }

    public virtual void InitializePermanentProjectileStats()
    {
        if (weaponData == null) return;

        var projUpgrade = weaponData.GetUpgrade(StatType.ProjectileCount);
        metaUpgradedProjectiles = projUpgrade != null && MetaUpgradeManager.Instance != null
            ? Mathf.RoundToInt(MetaUpgradeManager.Instance.GetStatValue(projUpgrade))
            : 1;
    }

    public int GetCurrentProjectileCount() => metaUpgradedProjectiles + inRunBonusProjectiles;

    public void IncreaseInRunProjectiles(int amount)
    {
        inRunBonusProjectiles += amount;
    }
}