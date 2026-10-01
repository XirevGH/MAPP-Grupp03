using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Saxophone : ProjectileWeapon
{
    [Header("Saxophone Components & Settings")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private float projectileSpeed = 12f;

    private readonly HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

    protected override void Start()
    {
        base.Start();

        if (shootingPoint == null) shootingPoint = transform;

        if (weaponData != null && weaponData.HasBeatTrigger)
        {
            UnityAction action = new UnityAction(Attack);
            TriggerController.Instance.SetTrigger(weaponData.BeatNumber, action);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
        if (!gameObject.activeSelf || notePrefab == null) return;

        enemiesInRange.RemoveWhere(e => e == null);

        if (enemiesInRange.Count == 0) return;

        List<GameObject> targets = FindClosestEnemies(GetCurrentProjectileCount());
        if (targets.Count > 0)
        {
            if (weaponData != null && weaponData.AttackSound != null)
            {
                SoundManager.Instance.PlaySFX(weaponData.AttackSound, 0.3f);
            }

            foreach (GameObject target in targets)
            {
                ShootNoteAtEnemy(target);
            }
        }
    }

    private List<GameObject> FindClosestEnemies(int targetCount)
    {
        if (enemiesInRange.Count == 0 || targetCount <= 0)
            return new List<GameObject>();

        Vector3 origin = shootingPoint.position;

        return enemiesInRange
            .Where(e => e != null)
            .OrderBy(e => (e.transform.position - origin).sqrMagnitude)
            .Take(targetCount)
            .ToList();
    }

    private void ShootNoteAtEnemy(GameObject enemy)
    {
        if (enemy == null || notePrefab == null) return;

        Vector3 spawnPos = shootingPoint.position;
        GameObject note = Instantiate(notePrefab, spawnPos, Quaternion.identity);

        if (note.TryGetComponent<NoteProjectile>(out var noteProjectile))
        {
            Vector3 direction = (enemy.transform.position - spawnPos).normalized;

            noteProjectile.Initialize(
                GetCurrentDamage(),
                projectileSpeed,
                GetCurrentPenetration(),
                direction
            );
        }
    }
}