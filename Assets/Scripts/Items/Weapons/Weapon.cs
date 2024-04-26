
public abstract class Weapon : Item
{
    public float damage;
    public float percentageDamageIncreasePerUpgrade;

    public abstract void Attack();

    public virtual void IncreaseDamage()
    {
        damage *= percentageDamageIncreasePerUpgrade;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
    }
}
