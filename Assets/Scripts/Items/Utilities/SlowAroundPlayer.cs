using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAroundPlayer : Utility
{
    [SerializeField] private float slowSpeedPercent;
    private HashSet<GameObject> enemies = new HashSet<GameObject>();

    public void UpgradeRradius( float percent)
    {
        gameObject.transform.localScale *= (1 + (percent / 100f));
    }
    public void UpgradeSlow(float percent)
    {
        slowSpeedPercent *= (1 + (percent/100f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().thisMovementSpeed = other.gameObject.GetComponent<Enemy>().thisMovementSpeed * slowSpeedPercent;
            other.gameObject.GetComponent<Enemy>().isSlow = true;
            enemies.Add(other.gameObject);  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().isSlow = false;
            //other.gameObject.GetComponent<Enemy>().thisMovementSpeed = Enemy.movementSpeed + other.GetComponent<Enemy>().baseMovementSpeed ;
            enemies.Remove(other.gameObject);
        }
    }
}
