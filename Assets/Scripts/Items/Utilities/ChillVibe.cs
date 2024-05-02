using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillVibe : Utility
{
    [SerializeField] private float slowSpeedPercent;
    [SerializeField] private float radiusIncreasePercentage;
    [SerializeField] private float slowIncreasePercentage;

    public int slowRank;
    public int radiusRank;

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < slowRank; i++)
        {
            IncreaseSlow();
        }
        for (int i = 0; i < radiusRank; i++)
        {
            IncreaseRadius();
        }
        EndInitialUpgrades();
    }

    public void RadiusUpgradeRank(int rankAmount)
    {
        radiusRank += rankAmount;
    }

    public void SlowUpgradeRank(int rankAmount)
    {
        slowRank += rankAmount;
    }

    public void IncreaseRadius()
    {
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
        if (InitialUpgradesComplete())
        {
            RadiusUpgradeRank(1);
        }
    }

    public void IncreaseSlow()
    {
        slowSpeedPercent *= (1 - (slowIncreasePercentage / 100f));
        if (InitialUpgradesComplete())
        {
            SlowUpgradeRank(1);
        }
    }

    public float GetRadiusIncreasePercentage()
    {
        return radiusIncreasePercentage;
    }

    public float GetSlowIncreasePercentage()
    {
        return slowIncreasePercentage;
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseRadius");
        upgradeOptions.Add("IncreaseSlow");
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
