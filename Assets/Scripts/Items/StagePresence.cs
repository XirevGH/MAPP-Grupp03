using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePresence : Weapon
{

    [SerializeField] private float radiusIncreasePercentage;

    public int radiusRank;

    private float damageInterval = 3f; //ska vara med beatet
    private float timer = 0f;

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


    /*
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

    }*/

    private void Update()
    {
        timer += Time.deltaTime;

        float banana = (60f / TriggerController.Instance.GetCurrentTrackBPM());

        if (timer >= banana)
        {
            timer = 0f;
            DealDamage();
        }
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
