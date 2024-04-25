using System.Collections.Generic;

public abstract class Weapon : Item
{
    public float damage;
    public float damageIncreasePerUpgrade;

    public abstract void Attack();

    public void IncreaseDamage()
    {
        damage *= damageIncreasePerUpgrade;
    }

    public override List<string> GetUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
        return upgradeOptions;
    }
}
