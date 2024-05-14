using System;
using UnityEngine;

public class StagePresence : Weapon
{

    [SerializeField] private float radiusIncreasePercentage;

    public int radiusRank;
    public int radiusUpgradeCost;


    /* private float damageInterval = (60f / TriggerController.Instance.GetCurrentTrackBPM()); //ska vara med beatet*/
    private float timer = 0f;

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
        return (float)Math.Round((Mathf.Pow(1 + (radiusIncreasePercentage / 100f), radiusRank) - 1) * 100, 1);
    }

    public int GetIncreaseRadiusCost()

    {
        return radiusUpgradeCost;
    }

    public int GetRadiusUpgradeRank()
    {
        return radiusRank;
    }

    protected override void CreateUpgradeOptions()
    {
        base.CreateUpgradeOptions();
        upgradeOptions.Add("IncreaseRadius");
    }


    private void Update()
    {
        timer += Time.deltaTime;

        /*if (timer >= damageInterval)
        {*/
            timer = 0f;
            DealDamage();
        /*}*/
    }


    private void DealDamage()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius * 1.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    
}
