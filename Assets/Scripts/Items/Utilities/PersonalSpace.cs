using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonalSpace : Utility
{
    [Header("Components")]
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private CircleCollider2D spaceCollider;

    private float metaUpgradedRadius;
    private float metaUpgradedForce;

    private float inRunRadiusMultiplier = 1f;
    private float inRunForceMultiplier = 1f;

    private bool pushingBack = false;

    private HashSet<GameObject> collidersInZone = new HashSet<GameObject>();

    private ParticleSystem.MainModule mainModule;


    private void Start()
    {
        if (spaceCollider == null) spaceCollider = GetComponent<CircleCollider2D>();
        if (ps != null) mainModule = ps.main;

        InitializeStats();

        if (utilityData != null && utilityData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(PushEnemiesAway);
            TriggerController.Instance.SetTrigger(utilityData.BeatNumber, action);
        }

        mainModule.startLifetime = 0.1f;
    }

    public void InitializeStats()
    {
        if (utilityData == null) return;

        var forceUpgrade = utilityData.GetUpgrade(StatType.Force);
        metaUpgradedForce = forceUpgrade != null && MetaUpgradeManager.Instance != null
            ? MetaUpgradeManager.Instance.GetStatValue(forceUpgrade)
            : 5;

        var radiusUpgrade = utilityData.GetUpgrade(StatType.Radius);
        metaUpgradedRadius = radiusUpgrade != null && MetaUpgradeManager.Instance != null
            ? MetaUpgradeManager.Instance.GetStatValue(radiusUpgrade)
            : 2.5f;
        ApplyVisualRadius();
    }

    private void ApplyVisualRadius()
    {
        float finalRadius = GetCurrentRadius();
        transform.localScale = Vector3.one * finalRadius;

        if (ps != null)
        {
            mainModule.startLifetime = transform.localScale.x / 10f;
        }
    }

    private void FixedUpdate()
    {
        if (pushingBack)
        {
            float finalForce = GetCurrentForce();

            foreach (GameObject enemyObj in collidersInZone)
            {
                if (enemyObj != null && enemyObj.CompareTag("Enemy"))
                {
                    if (enemyObj.TryGetComponent<Rigidbody2D>(out var enemyRB))
                    {
                        Vector2 direction = (enemyObj.transform.position - transform.position).normalized;

                        if (enemyObj.TryGetComponent<Enemy>(out var enemyScript))
                        {
                            enemyScript.isPushedBack = true;
                        }

                        enemyRB.AddRelativeForce(direction * finalForce, ForceMode2D.Force);
                        StartCoroutine(RemoveForce(enemyRB, 1f));
                    }
                }
            }
        }
    }

    private void PushEnemiesAway()
    {
        if (gameObject.activeSelf) 
        {
            collidersInZone.Clear();
            if (spaceCollider != null) spaceCollider.enabled = true;
            if (ps != null)
            {
                ps.Play();
                ParticleSystem.EmissionModule em = ps.emission;
                em.enabled = true;
            }

            pushingBack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        collidersInZone.Add(other.gameObject);
    }


    private IEnumerator RemoveForce(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (rb != null)
        {
            if (spaceCollider != null) spaceCollider.enabled = false;
            pushingBack = false;

            if (rb.TryGetComponent<Enemy>(out var enemyScript))
            {
                enemyScript.isPushedBack = false;
            }

            rb.velocity = Vector2.zero;
        }
    }

    public float GetCurrentRadius() => metaUpgradedRadius * inRunRadiusMultiplier;
    public float GetCurrentForce() => metaUpgradedForce * inRunForceMultiplier;

    public void IncreaseInRunRadius(float percentage)
    {
        inRunRadiusMultiplier *= (1f + (percentage / 100f));
        ApplyVisualRadius();
    }

    public void IncreaseInRunForce(float percentage)
    {
        inRunForceMultiplier *= (1f + (percentage / 100f));
    }
}