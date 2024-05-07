using System.Collections;
using System.Collections.Generic;
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

    public float GetRadiusUpgradePercentage()
    {
        return radiusIncreasePercentage;
    }

    public float GetSlowUpgradePercentage()
    {
        return slowIncreasePercentage;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().thisMovementSpeed = other.gameObject.GetComponent<Enemy>().thisMovementSpeed * slowSpeedPercent;
            other.gameObject.GetComponent<Enemy>().isSlow = true;
          
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
}
