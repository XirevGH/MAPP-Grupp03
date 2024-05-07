using UnityEngine;

public class StagePresence : Weapon
{
    [SerializeField] private float radiusIncreasePercentage;

    public int radiusRank;
    public int radiusUpgradeCost;

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
    }

    public float GetRadiusIncreasePercentage()
    {
        return radiusIncreasePercentage;
    }

    public float GetCurrentRadiusIncrease()
    {
        return Mathf.Pow(1 + (radiusIncreasePercentage / 100f), radiusRank);
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseRadius");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }  
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public int GetIncreaseRadiusCost()
    {
        return radiusUpgradeCost;
    }

    public int GetRadiusUpgradeRank()
    {
        return radiusRank;
    }
}
