using System;
using UnityEngine;

public class ChillVibe : Utility
{
    [SerializeField] private float slowSpeedPercent;
    [SerializeField] private float radiusIncreasePercentage;
    [SerializeField] private float slowIncreasePercentage;

    public int slowRank;
    public int slowUpgradeCost;
    public int radiusRank;
    public int radiusUpgradeCost;

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
    }

    public void IncreaseSlow()
    {
        slowRank++;
        slowSpeedPercent *= (1 - (slowIncreasePercentage / 100f));
    }

    public float GetRadiusIncreasePercentage()
    {
        return (float)Math.Round(radiusIncreasePercentage, 1);
    }

    public float GetSlowIncreasePercentage()
    {
        return (float)Math.Round(slowIncreasePercentage, 1);
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseRadius");
        upgradeOptions.Add("IncreaseSlow");
    }

    public int GetIncreaseRadiusCost()
    {
        return radiusUpgradeCost;
    }

    public int GetIncreaseSlowCost()
    {
        return slowUpgradeCost;
    }

    public float GetCurrentSlowIncrease()
    {
        
        return (float)Math.Round(100 - (slowSpeedPercent * 100), 1);
    }

    public float GetCurrentRadiusIncrease()
    {
        
        return (float)Math.Round((Mathf.Pow(1 + (radiusIncreasePercentage / 100f), radiusRank) - 1) * 100, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            Enemy  enemyScript = other.gameObject.GetComponent<Enemy>();
            enemyScript.GetComponent<Enemy>().isSlow = true;
            enemyScript.thisMovementSpeed = enemyScript.thisMovementSpeed * slowSpeedPercent * Enemy.movementSpeed * enemyScript.baseMovementSpeed;
           
          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().isSlow = false;
            //other.gameObject.GetComponent<Enemy>().thisMovementSpeed = Enemy.movementSpeed + other.GetComponent<Enemy>().baseMovementSpeed ;
           
        }
    }

    public int GetRadiusUpgradeRank()
    {
        return radiusRank;
    }

    public int GetSlowUpgradeRank()
    {
        return slowRank;
    }
}
