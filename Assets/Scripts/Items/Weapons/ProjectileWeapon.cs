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

    public int GetProjectilePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    public int GetPenetrationPerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();

        upgradeOptions.Add("IncreaseProjectileCount");
        upgradeOptions.Add("IncreasePenetrationAmount");
    }

    public abstract int GetIncreaseProjectileCountCost();

    public abstract int GetIncreasePenetrationAmountCost();
}
