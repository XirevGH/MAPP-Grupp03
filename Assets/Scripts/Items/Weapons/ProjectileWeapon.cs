using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;
    [SerializeField] protected int penetration;
    [SerializeField] protected int penetrationIncreasePerUpgrade;
    public int projectileRank;
    public int projectileUpgradeCost;
    public int penetrationRank;
    public int penetrationUpgradeCost;

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();

        upgradeOptions.Add("IncreaseProjectileCount");
        upgradeOptions.Add("IncreasePenetrationAmount");
    }

    public void IncreaseProjectileCount()
    {
        projectileRank++;
        amountOfProjectiles += projectileIncreasePerUpgrade;
    }

    public void IncreasePenetrationAmount()
    {
        penetrationRank++;
        penetration += penetrationIncreasePerUpgrade;
    }

    public int GetProjectileIncreasePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    public int GetPenetrationIncreasePerUpgrade()
    {
        return penetrationIncreasePerUpgrade;
    }

    public int GetIncreaseProjectileCountCost()
    {
        return projectileUpgradeCost;
    }

    public int GetIncreasePenetrationAmountCost()
    {
        return penetrationUpgradeCost;
    }

    public int GetProjectileUpgradeRank()
    {
        return projectileRank;
    }

    public int GetPenetrationUpgradeRank()
    {
        return penetrationRank;
    }

    public int GetCurrentProjectileCount()
    {
        return amountOfProjectiles;
    }

    public int GetCurrentPenetrationAmount()
    {
        return penetration;
    }
}
