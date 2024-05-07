using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePresence : Weapon
{

    [SerializeField] private float radiusIncreasePercentage;

    public int radiusRank;

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
    }

    public float GetRadiusUpgradePercentage()
    {
        return radiusIncreasePercentage;
    }

    protected override void CreateUpgradeOptions()
    {
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
    
}
