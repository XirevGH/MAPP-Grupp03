using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonalSpace : Utility
{
    [SerializeField] private ParticleSystem ps;
    public float pushForce = 5f;

    [SerializeField] private float radiusIncreasePercentage;
    [SerializeField] private float forceIncreasePercentage;

    public int radiusRank;
    public int forceRank;

    public int radiusUpgradeCost;
    public int forceUpgradeCost;

    private bool pushingBack;

    private CircleCollider2D spaceCollider;
    private HashSet<GameObject> colliders;

    ParticleSystem.MainModule mainModule;


    private void Start()
    {
        UnityAction action = new UnityAction(PushEnemiesAway);

        TriggerController.Instance.SetTrigger(4, action);
        spaceCollider = GetComponent<CircleCollider2D>();
        colliders = new HashSet<GameObject>();
        mainModule = ps.main;
        mainModule.startLifetime = 0.1f;
    }

    private void FixedUpdate()
    {
        if (pushingBack)
        {
            List<GameObject> copyOfColliders = new List<GameObject>(colliders);
            foreach (GameObject collider in copyOfColliders)
            {
                if (collider != null)
                {
                    if (collider.gameObject.CompareTag("Enemy"))
                    {
                        Vector2 direction = collider.transform.position - transform.position;

                        direction.Normalize();

                        Rigidbody2D enemyRB = collider.gameObject.GetComponent<Rigidbody2D>();
                        if (enemyRB != null)
                        {
                            collider.gameObject.GetComponent<Enemy>().isPushedBack = true;
                            enemyRB.AddRelativeForce(direction * pushForce, ForceMode2D.Force);
                            StartCoroutine(RemoveForce(enemyRB, 1f));
                        }
                    }
                }
            }
        }
    }

    public void IncreaseRadius()
    {
        radiusRank++;
        gameObject.transform.localScale *= (1 + (radiusIncreasePercentage / 100f));
        mainModule.startLifetime = gameObject.transform.localScale.x / 10;
    }

    public float GetCurrentRadiusIncrease()
    {
        return (float)Math.Round((Mathf.Pow(1 + (radiusIncreasePercentage / 100f), radiusRank) - 1) * 100, 1);
    }

    public float GetCurrentForceIncrease()
    {
        return (float)Math.Round((Mathf.Pow(1 + (forceIncreasePercentage / 100f), forceRank) - 1) * 100, 1);
    }

    public void IncreaseForce()
    {
        forceRank++;
        pushForce *= (1 + (forceIncreasePercentage / 100f));
    }

    public float GetRadiusIncreasePercentage()
    {
        return radiusIncreasePercentage;
    }


    public float GetForceIncreasePercentage()
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
        if (gameObject.activeSelf) 
        {
            spaceCollider.enabled = true;
            ps.Play(); 
            ParticleSystem.EmissionModule em = ps.emission; 
            em.enabled = true;

            pushingBack = true;
        }
    }

/*    private void OnTriggerEnter2D(Collider2D other)
    {
        colliders.Add(other.gameObject);
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        colliders.Add(other.gameObject);
    }


    private System.Collections.IEnumerator RemoveForce(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(rb != null)
        {
            spaceCollider.enabled = false;
            pushingBack = false;
            rb.gameObject.GetComponent<Enemy>().isPushedBack = false;
            rb.velocity = Vector2.zero;
        }
    }

    public int GetRadiusUpgradeRank()
    {
        return radiusRank;
    }

    public int GetForceUpgradeRank()
    {
        return forceRank;
    }
}