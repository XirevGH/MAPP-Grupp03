using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillVibe : Utility
{
    [SerializeField] private float slowSpeedPercent;


    public void UpgradeRradius()
    {
        gameObject.transform.localScale *= (1 + (percentage / 100f));
    }

    public void UpgradeSlow()
    {
        slowSpeedPercent *= (1 - (percentage / 100f));
    }

    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("UpgradeRradius");
        upgradeOptions.Add("UpgradeSlow");
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
