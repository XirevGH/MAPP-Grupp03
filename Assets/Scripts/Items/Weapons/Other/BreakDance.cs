using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakDance : Weapon
{
    [SerializeField] private Animator anim;

    private float metaUpgradedRadius = 1f;
    private float inRunRadiusMultiplier = 1f;

    private readonly HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

    protected override void Start()
    {
        base.Start();

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

        var radiusUpgrade = weaponData.GetUpgrade(StatType.Radius);
        if (radiusUpgrade != null && MetaUpgradeManager.Instance != null)
        {
            metaUpgradedRadius = MetaUpgradeManager.Instance.GetStatValue(radiusUpgrade);
        }
        else
        {
            metaUpgradedRadius = 1f;
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

        // Purge dead enemies destroyed while inside the trigger zone
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

    public float GetCurrentRadius() => metaUpgradedRadius * inRunRadiusMultiplier;

    public void IncreaseInRunRadius(float percentage)
    {
        inRunRadiusMultiplier *= (1f + (percentage / 100f));
        ApplyRadiusScale();
    }
}