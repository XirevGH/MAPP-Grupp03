using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    [SerializeField] protected int amountOfProjectiles;
    [SerializeField] protected int projectileIncreasePerUpgrade;
    [SerializeField] protected int penetration;
    [SerializeField] protected int penetrationIncreasePerUpgrade;

    public void IncreaseProjectileCount()
    {
        amountOfProjectiles += projectileIncreasePerUpgrade;
    }
    public void IncreaseProjectileCount(int projectileIncrease) //felix
    {
        amountOfProjectiles += projectileIncrease;
    }

    public void IncreasePenetrationAmount()
    {
        penetration += penetrationIncreasePerUpgrade;
    }
    public void IncreasePenetrationAmount(int penetrationIncrease) //felix
    {
        penetration += penetrationIncrease;
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
