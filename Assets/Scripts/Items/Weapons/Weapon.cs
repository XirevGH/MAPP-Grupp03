
public abstract class Weapon : Item
{
    public float damage;
    public float percentageDamageIncrease;

    public abstract void Attack();

    public virtual void IncreaseDamage()
    {
        damage *= (1 + (percentageDamageIncrease / 100f));
    }

    public float GetDamageIncreasePercentage()
    {
        return percentageDamageIncrease;
    }
    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseDamage");
    }
}
