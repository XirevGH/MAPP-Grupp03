using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakDance : Weapon
{
    [SerializeField] private Animator anim;

    // Runtime Cached Radius Stats
    private float metaUpgradedRadius = 1f;
    private float inRunRadiusMultiplier = 1f;

    private readonly HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

    protected override void Start()
    {
        base.Start(); // ◄── CRITICAL: Initializes meta damage!

        if (anim == null) anim = GetComponent<Animator>();

        InitializeRadiusStats();

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, action);
        }
    }

    public void InitializeRadiusStats()
    {
        if (weaponData == null) return;

        // Query Radius meta-upgrade
        var radiusUpgrade = weaponData.GetUpgrade(StatType.Radius);
        if (radiusUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            // Gets base radius value (e.g. 1.0) + meta rank bonuses
            metaUpgradedRadius = MetaUpgradeManager.Instance.GetStatValue(radiusUpgrade);
        }
        else
        {
            metaUpgradedRadius = 1f; // Default baseline scale
        }

        ApplyRadiusScale();
    }

    private void ApplyRadiusScale()
    {
        transform.localScale = Vector3.one * GetCurrentRadius();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    public override void Attack()
    {
        if (!gameObject.activeSelf) return;

        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        if (weaponData != null && weaponData.AttackSound != null)
        {
            SoundManager.Instance.PlaySFX(weaponData.AttackSound, 1f);
        }

        // Purge dead enemies killed by other attacks
        enemiesInRange.RemoveWhere(e => e == null);

        float finalDamage = GetCurrentDamage();

        foreach (GameObject enemyObj in enemiesInRange)
        {
            if (enemyObj != null && enemyObj.TryGetComponent<Enemy>(out var enemyScript))
            {
                enemyScript.TakeDamage(finalDamage);
            }
        }
    }

    // Public Getters & Modifiers
    public float GetCurrentRadius() => metaUpgradedRadius * inRunRadiusMultiplier;

    // Called when picking an in-run level-up card (e.g. "+15% Breakdance Radius")
    public void IncreaseInRunRadius(float percentage)
    {
        inRunRadiusMultiplier *= (1f + (percentage / 100f));
        ApplyRadiusScale();
    }
}