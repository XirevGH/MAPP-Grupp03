using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;
    [SerializeField] protected int penetration;
    [SerializeField] protected int penetrationIncreasePerUpgrade;
    public int projectileRank;
    public int penetrationRank;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < projectileRank; i++)
        {
            IncreaseProjectileCount();
        }
        for (int i = 0; i < penetrationRank; i++)
        {
            IncreasePenetrationAmount();
        }
        EndInitialUpgrades();
    }

    public void ProjectileCountUpgradeRank(int rankAmount)
    {
        projectileRank += rankAmount;
    }

    public void PenetrationAmountUpgradeRank(int rankAmount)
    {
        penetrationRank += rankAmount;
    }

    public void IncreaseProjectileCount()
    {
        amountOfProjectiles += projectileIncreasePerUpgrade;
        if (InitialUpgradesComplete())
        {
            ProjectileCountUpgradeRank(1);
        }
    }

    public void IncreasePenetrationAmount()
    {
        penetration += penetrationIncreasePerUpgrade;
        if (InitialUpgradesComplete())
        {
            PenetrationAmountUpgradeRank(1);
        }
    }

    public int GetProjectileIncreasePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    public int GetPenetrationIncreasePerUpgrade()
    {
        return projectileIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();

        upgradeOptions.Add("IncreaseProjectileCount");
        upgradeOptions.Add("IncreasePenetrationAmount");
    }
}
