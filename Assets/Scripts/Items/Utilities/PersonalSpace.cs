using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalSpace : Utility
{
  
    public float pushForce = 5f;
    public float pushInterval = 10f;

    private float timer = 0f;


    [SerializeField] private float radiusIncreasePercentage;
    [SerializeField] private float forceIncreasePercentage;
    [SerializeField] public float radius;

    public int radiusRank;
    public int forceRank;

    public int radiusUpgradeCost;
    public int forceUpgradeCost;


    private void Update()
    {

        timer += Time.deltaTime;


        if (timer >= pushInterval)
        {
            PushEnemiesAway();
            timer = 0f;
        }
    }

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
        radius = gameObject.transform.localScale.x;
    }

    public void IncreaseForce()
    {
        forceRank++;
        pushForce *= (1 + (forceIncreasePercentage / 100f));
    }

    public float GetRadiusUpgradePercentage()
    {
        return radiusIncreasePercentage;
    }


    public float GetForceUpgradePercentage()
    {
        return forceIncreasePercentage;
    }
    protected override void CreateUpgradeOptions()
    {
        upgradeOptions.Add("IncreaseRadius");
        upgradeOptions.Add("IncreaseForce");
    }

    public int GetIncreaseRadiusCost()
    {
        return radiusUpgradeCost;
    }

    public int GetIncreaseForceCost()
    {
        return forceUpgradeCost;
    }

    private void PushEnemiesAway()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Vector2 direction = collider.transform.position - transform.position;

                direction.Normalize();

                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * pushForce, ForceMode2D.Impulse);
                    StartCoroutine(RemoveForce(rb, 1f));
                }
            }
        }
    }

    private System.Collections.IEnumerator RemoveForce(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

}

