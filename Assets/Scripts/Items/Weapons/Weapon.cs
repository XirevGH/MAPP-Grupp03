using System;
using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] protected WeaponDataSO weaponData;

    public override ItemDefinitionSO BaseItemData => weaponData;

    protected float metaUpgradedDamage;

    private float inRunDamageMultiplier = 1f;

    protected virtual void Start()
    {
        InitializeStartingStats();
    }

    public void InitializeStartingStats()
    {
        if (weaponData == null)
        {
            return;
        }

        float baseDmg = weaponData.BaseDamage;

        UpgradeDefinitionSO dmgUpgrade = weaponData.GetUpgrade(StatType.Damage);

        if (dmgUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            float bonusPercent = MetaUpgradeManager.Instance.GetStatValue(dmgUpgrade);
            baseDmg *= (1f + (bonusPercent / 100f));
        }

        metaUpgradedDamage = baseDmg;
        inRunDamageMultiplier = 1f;
    }

    public abstract void Attack();

    public virtual void IncreaseInRunDamage(float percent)
    {
        inRunDamageMultiplier *= (1f + (percent / 100f));
    }

    public float GetCurrentDamage()
    {
        return metaUpgradedDamage * inRunDamageMultiplier;
    }
}
